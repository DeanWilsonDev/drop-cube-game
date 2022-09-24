using System.Collections.Generic;
using UnityEngine;
using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;

namespace BlackPad.DropCube.Level {
  public class FloorGenerator : Generator, IGenerator<Floor> {

    const float MinOffset = 0;
    const float MaxOffset = 3;
    const string FloorGameObjectName = "Floor";
    const int LeftSide = 0;
    const int RightSide = 1;

    Floor floor;
    GameObject[] floorObjects;
    
    readonly float doorSize;
    readonly float roomWidth;

    public FloorGenerator(Component parent, float roomWidth, float doorSize) {
      this.Parent = parent;
      this.roomWidth = roomWidth;
      this.doorSize = doorSize;
    }

    public GameObject SetupFloorPortion(float size) {
      var floorSide = GameObject.CreatePrimitive(PrimitiveType.Cube);
      var parentTransform = Parent.transform;
      floorSide.transform.position = parentTransform.position;
      floorSide.transform.localScale = new Vector3(size, 1, 5);
      floorSide.transform.parent = parentTransform;
      return floorSide;
    }

    public float[] SelectDoorSide() {
      var isSelected = Utilities.DetermineIfRandomlySelected(50);
      var doorFlags = new[] {
        isSelected,
        !isSelected
      };
      return ApplyDoorAllocation(doorFlags);
    }

    float[] ApplyDoorAllocation(IReadOnlyList<bool> doorFlags) {
      var floorAllocation = new float[2];
      for (var i = 0; i < 2; i++) {
        floorAllocation[i] = roomWidth / 2;
        floorAllocation[i] -= Utilities.GetValueIfTrue(doorFlags[i], doorSize);
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
      var isSelected = Utilities.DetermineIfRandomlySelected(50 );
      var offsetFlags = new[] {
        isSelected,
        !isSelected
      };
      var offset = Random.Range(MinOffset, roomWidth / MaxOffset);
      return ApplyOffset(offset, offsetFlags, floorAllocation);
    }

    void GenerateFloorObjects() {
      floorObjects = new GameObject[2];
      var floorAllocation = SelectDoorSide();
      var offsetFloorAllocation = SelectOffsetSide(floorAllocation);
      for (var i = 0; i < 2; i++) {
        floorObjects[i] = SetupFloorPortion(offsetFloorAllocation[i]);
        floorObjects[i]
          .name = FloorGameObjectName + (i + 1);
      }
    }

    Vector3 GetLeftSideTransformPosition() =>
      new(
        Utilities.GameObjectTransformPosition(floorObjects[LeftSide])
          .x
        + Utilities.GameObjectWidth(floorObjects[LeftSide]) / 2,
        Utilities.GameObjectTransformPosition(floorObjects[LeftSide])
          .y,
        Utilities.GameObjectTransformPosition(floorObjects[LeftSide])
          .z
      );

    Vector3 GetRightSideTransformPosition() {
      var position = Parent.transform.position;
      return new Vector3(
        Utilities.FindOriginPoint(floorObjects[LeftSide])
        + Utilities.GameObjectWidth(floorObjects[LeftSide])
        + doorSize
        + (Utilities.GameObjectWidth(floorObjects[RightSide]) / 2),
        position.y,
        position.z
      );
    }
    void SetFloorParentAndReference() {
      foreach (var floorObject in floorObjects) {
        floorObject.transform.parent = floor.transform;
        floor
          .floorGameObjects
          .Add(floorObject);
      }
    }
    
    public void SetPosition() {
      floorObjects[LeftSide]
        .transform.position = GetLeftSideTransformPosition();
      floorObjects[RightSide]
        .transform.position = GetRightSideTransformPosition();
    }

    public Floor Initialize() {
      floor = this.Initialize<Floor>(FloorGameObjectName);
      return floor;
    }

    public void SetupPrefab(GameObject prefab) {
      GenerateFloorObjects();
      SetFloorParentAndReference();
    }

  }
}