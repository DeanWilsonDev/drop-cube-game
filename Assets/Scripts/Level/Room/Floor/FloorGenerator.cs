using System.Collections.Generic;
using UnityEngine;

namespace BlackPad.DropCube.Level {
  public static class FloorGenerator {

    const float MinOffset = 0;
    const float MaxOffset = 5;
    
    public static GameObject SetupFloorPortion(Component parent, float size) {
      var floorSide = GameObject.CreatePrimitive(PrimitiveType.Cube);
      var parentTransform = parent.transform;
      floorSide.transform.position = parentTransform.position;
      floorSide.transform.localScale = new Vector3(size, 1, 5);
      floorSide.transform.parent = parentTransform;
      return floorSide;
    }

    public static bool IsRandomlySelected(int randomValue) => randomValue < 50;

    public static float GetValueIfTrue(bool flag, float value) => 
      flag
        ? value
        : 0;
    
    static bool DetermineIfRandomlySelected() {
      var randomSelection = Random.Range(0, 100);
      return IsRandomlySelected(randomSelection);
    }

    public static float[] SelectDoorSide(float availableSpace, GameObject door) {
      var isSelected = DetermineIfRandomlySelected();
      var doorFlags = new[] {
        isSelected,
        !isSelected
      };
      return ApplyDoorAllocation(availableSpace, doorFlags, door);
    }

    public static float[] ApplyDoorAllocation(float availableSpace, bool[] doorFlags, GameObject door) {
      var floorAllocation = new float[2];
      for (var i = 0; i < 2; i++) {
        floorAllocation[i] = availableSpace / 2;
        floorAllocation[i] -= GetValueIfTrue(doorFlags[i], door.transform.localScale.x);
      }
      return floorAllocation;
    }


    public static float[] ApplyOffset(float offset, bool[] offsetFlags, float[] floorAllocation) {
      for (var i = 0; i < 2; i++) {
        floorAllocation[i] = offsetFlags[i] 
          ? floorAllocation[i] - offset 
          : floorAllocation[i] + offset;
      }
      return floorAllocation;
    }
    
    public static float[] SelectOffsetSide(float[] floorAllocation) {
      var isSelected = DetermineIfRandomlySelected();
      var offsetFlags = new[] {
        isSelected,
        !isSelected
      };
      var offset = Random.Range(MinOffset, MaxOffset);
      return ApplyOffset(offset, offsetFlags, floorAllocation);
    }

    public static GameObject[] GenerateFloorObjects(Component parent, float availableSpace, GameObject door) {
      var floors = new GameObject[2];
      var floorAllocation = SelectDoorSide(availableSpace, door);
      var offsetFloorAllocation = SelectOffsetSide(floorAllocation);
      for (var i = 0; i < 2; i++) {
        floors[i] = SetupFloorPortion(parent, offsetFloorAllocation[i]);
        floors[i]
          .name = "Floor" + (i + 1);
      }

      return floors;
    }

    public static Vector3 GetLeftSideTransformPosition(GameObject leftSide) {
      var leftSideTransformPosition = leftSide.transform.position;
      var leftSideLocalScale = leftSide.transform.localScale;
      return new Vector3(
        leftSideTransformPosition.x + (leftSideLocalScale.x / 2),
        leftSideTransformPosition.y,
        leftSideTransformPosition.z
      );
    }

    static Vector3 GetRightSideTransformPosition(
      Vector3 parentPosition,
      GameObject rightSide,
      GameObject leftSide,
      float doorSize
    ) => new (
        FindOriginPoint(leftSide)
        + leftSide.transform.localScale.x
        + doorSize
         + (rightSide.transform.localScale.x / 2),
        parentPosition.y,
        parentPosition.z
      );
    
    static float FindOriginPoint(GameObject gObject) => gObject.transform.position.x - (gObject.transform.localScale.x / 2);

    public static IEnumerable<GameObject> GenerateFloorsAndDoors(
      Component parent,
      float availableSpace,
      GameObject door
    ) {
      var floors = GenerateFloorObjects(parent, availableSpace, door);
      
      var leftSide = floors[0];
      var rightSide = floors[1];
      
      leftSide.transform.position = GetLeftSideTransformPosition(leftSide);
      rightSide.transform.position = GetRightSideTransformPosition(
        parent.transform.position,
        rightSide,
        leftSide,
        door.transform.localScale.x
      );
      
      return new[] {
        leftSide, rightSide
      };
    }

    public static GameObject GenerateFloor(Component parent, float availableSpace, GameObject door) {
      var floorObjects = GenerateFloorsAndDoors(parent, availableSpace, door);
      var floor = new GameObject {
        name = "Floor"
      };
      floor.AddComponent<Floor>();
      foreach (var floorObject in floorObjects) {
        floorObject.transform.parent = floor.transform;
        
        floor
          .GetComponent<Floor>()
          .floorGameObjects
          .Add(floorObject);
      }
      return floor;
    }

    public static Floor InitializeFloor(Component parent, float availableSpace, GameObject door) {
      var floor = GenerateFloor(parent, availableSpace, door)
        .GetComponent<Floor>();
      floor.transform.parent = parent.transform;
      return floor;
    }
  }
}