using System.Collections.Generic;
using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Floor {
  public class FloorGenerator : Generator, IGenerator<Floor> {

    const float MIN_OFFSET = 0;
    const float MAX_OFFSET = 3;
    const string FLOOR_GAME_OBJECT_NAME = "Floor";
    const int LEFT_SIDE = 0;
    const int RIGHT_SIDE = 1;

    Floor floor;
    GameObject[] floorObjects;
    
    readonly float doorSize;
    readonly float roomWidth;

    public FloorGenerator(Component parent, float roomWidth, float doorSize) {
      this.Parent = parent;
      this.roomWidth = roomWidth;
      this.doorSize = doorSize;
    }

    GameObject SetupFloorPortion(float size) {
      var floorSide = GameObject.CreatePrimitive(PrimitiveType.Cube);
      var parentTransform = Parent.transform;
      floorSide.transform.position = parentTransform.position;
      floorSide.transform.localScale = new Vector3(size, 1, 5);
      floorSide.transform.parent = parentTransform;
      return floorSide;
    }

    float[] SelectDoorSide() {
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

    static float[] ApplyOffset(float offset, bool[] offsetFlags, float[] floorAllocation) {
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
      var offset = Random.Range(MIN_OFFSET, roomWidth / MAX_OFFSET);
      return ApplyOffset(offset, offsetFlags, floorAllocation);
    }

    void GenerateFloorObjects() {
      floorObjects = new GameObject[2];
      var floorAllocation = SelectDoorSide();
      var offsetFloorAllocation = SelectOffsetSide(floorAllocation);
      for (var i = 0; i < 2; i++) {
        floorObjects[i] = SetupFloorPortion(offsetFloorAllocation[i]);
        floorObjects[i]
          .name = FLOOR_GAME_OBJECT_NAME + (i + 1);
      }
    }

    Vector3 GetLeftSideTransformPosition() =>
      new(
        Utilities.GameObjectTransformPosition(floorObjects[LEFT_SIDE])
          .x
        + Utilities.GameObjectWidth(floorObjects[LEFT_SIDE]) / 2,
        Utilities.GameObjectTransformPosition(floorObjects[LEFT_SIDE])
          .y,
        Utilities.GameObjectTransformPosition(floorObjects[LEFT_SIDE])
          .z
      );

    Vector3 GetRightSideTransformPosition() {
      var position = Parent.transform.position;
      return new Vector3(
        Utilities.FindOriginPoint(floorObjects[LEFT_SIDE])
        + Utilities.GameObjectWidth(floorObjects[LEFT_SIDE])
        + doorSize
        + (Utilities.GameObjectWidth(floorObjects[RIGHT_SIDE]) / 2),
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

    public Floor GetGeneratedFloor() {
      return floor;
    }
    
    public void SetPosition() {
      floorObjects[LEFT_SIDE]
        .transform.position = GetLeftSideTransformPosition();
      floorObjects[RIGHT_SIDE]
        .transform.position = GetRightSideTransformPosition();
    }

    public Floor Initialize() {
      floor = this.Initialize<Floor>(FLOOR_GAME_OBJECT_NAME);
      return floor;
    }

    public void SetupPrefab(GameObject prefab) {
      GenerateFloorObjects();
      SetFloorParentAndReference();
    }

    public void SetColor(Color color) {
      var colorId = Shader.PropertyToID(
        "_Color"
      );
      floorObjects[0]
        .gameObject
        .GetComponent<Renderer>()
        .material
        .SetColor(
          colorId,
          color
        );
      floorObjects[1]
        .gameObject
        .GetComponent<Renderer>()
        .material
        .SetColor(
          colorId,
          color
        );
    }
  }
}