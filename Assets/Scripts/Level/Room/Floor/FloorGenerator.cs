using System.Collections.Generic;
using UnityEngine;

namespace BlackPad.DropCube.Level {
  public class FloorGenerator {

    const float MinOffset = 0;
    const float MaxOffset = 3;
    
    // Switch switch;
    readonly Component parent;

    float doorSize;
    readonly float availableSpace;

    public FloorGenerator(Component parent, float availableSpace, float doorSize) {
      this.parent = parent;
      this.availableSpace = availableSpace;
      this.doorSize = doorSize;
    }

    public GameObject SetupFloorPortion(float size) {
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

    public float[] SelectDoorSide() {
      var isSelected = DetermineIfRandomlySelected();
      var doorFlags = new[] {
        isSelected,
        !isSelected
      };
      return ApplyDoorAllocation(doorFlags);
    }

    float[] ApplyDoorAllocation(IReadOnlyList<bool> doorFlags) {
      SetupDoor();
      var floorAllocation = new float[2];
      for (var i = 0; i < 2; i++) {
        floorAllocation[i] = availableSpace / 2;
        floorAllocation[i] -= GetValueIfTrue(doorFlags[i], doorSize);
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

    float[] SelectOffsetSide(float[] floorAllocation) {
      var isSelected = DetermineIfRandomlySelected();
      var offsetFlags = new[] {
        isSelected,
        !isSelected
      };
      var offset = Random.Range(MinOffset, availableSpace / MaxOffset);
      return ApplyOffset(offset, offsetFlags, floorAllocation);
    }

    public GameObject[] GenerateFloorObjects() {
      var floors = new GameObject[2];
      var floorAllocation = SelectDoorSide();
      var offsetFloorAllocation = SelectOffsetSide(floorAllocation);
      for (var i = 0; i < 2; i++) {
        floors[i] = SetupFloorPortion(offsetFloorAllocation[i]);
        floors[i]
          .name = "Floor" + (i + 1);
      }

      return floors;
    }

    static Vector3 GetLeftSideTransformPosition(GameObject leftSide) {
      var leftSideTransformPosition = leftSide.transform.position;
      var leftSideLocalScale = leftSide.transform.localScale;
      return new Vector3(
        leftSideTransformPosition.x + (leftSideLocalScale.x / 2),
        leftSideTransformPosition.y,
        leftSideTransformPosition.z
      );
    }

    Vector3 GetRightSideTransformPosition(
      GameObject rightSide,
      GameObject leftSide
    ) {
      var position = parent.transform.position;
      return new Vector3(
        FindOriginPoint(leftSide)
        + leftSide.transform.localScale.x
        + doorSize
        + (rightSide.transform.localScale.x / 2),
        position.y,
        position.z
      );
    }

    static float FindOriginPoint(GameObject gObject) => gObject.transform.position.x - (gObject.transform.localScale.x / 2);

    IEnumerable<GameObject> GenerateFloorsAndDoors(Component door) {
      var floors = GenerateFloorObjects();

      var leftSide = floors[0];
      var rightSide = floors[1];

      var leftSideTransformPosition = GetLeftSideTransformPosition(leftSide);
      leftSide.transform.position = leftSideTransformPosition;
      doorSize = door.transform.localScale.x;
      rightSide.transform.position = GetRightSideTransformPosition(
        rightSide,
        leftSide
      );

      door.transform.position = new Vector3(
        leftSideTransformPosition.x + (leftSide.transform.localScale.x / 2) + (doorSize / 2),
        leftSideTransformPosition.y,
        leftSideTransformPosition.z
      );

      return new[] {
        leftSide, rightSide
      };
    }

    GameObject GenerateFloor() {
      var door = SetupDoor();
      var floorObjects = GenerateFloorsAndDoors(door);
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

    Door SetupDoor() {
      var doorGenerator = new DoorGenerator(parent, doorSize);
      return doorGenerator.InitializeDoor();
    }

    public Floor InitializeFloor() {
      var floor = GenerateFloor()
        .GetComponent<Floor>();
      floor.transform.parent = parent.transform;
      return floor;
    }

  }
}