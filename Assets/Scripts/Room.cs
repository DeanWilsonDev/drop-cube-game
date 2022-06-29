using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

  GameObject _leftSide;
  GameObject _rightSide;

  float _totalRoomWidth = 25;
  float _doorSize = 3f;

  public float RoomHeight { get; } = 10f;

  // Start is called before the first frame update
  void Start() => SetupRoom();

  GameObject SetupFloorPortion(float size) {
    var floorSide = GameObject.CreatePrimitive(PrimitiveType.Cube);
    floorSide.transform.position = gameObject.transform.position;
    floorSide.transform.localScale = new Vector3(size, 1, 5);
    floorSide.transform.parent = transform;
    return floorSide;
  }

  GameObject SetupWall() {
    var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
    wall.transform.localScale = new Vector3(1, RoomHeight, 5);
    var parent = transform;
    var parentPosition = parent.position;
    wall.transform.position = new Vector3(parentPosition.x, parentPosition.y + (wall.transform.localScale.y / 2), parentPosition.z );
    wall.transform.parent = parent;
    return wall;
  }

  void SetupRoom() {
    var availableSpace = (_totalRoomWidth - _doorSize) / 2;
    SetupFloorsAndDoors(availableSpace);
    SetupWalls();
  }


  GameObject[] GenerateFloorObjects(float availableSpace) {
    var offset = Random.Range(0, availableSpace / 2 - _doorSize);
    var floorAllocation = availableSpace - offset - _doorSize;

    var floors = new GameObject[2];
    for (var i = 0; i < 2; i++) {
      floors[i] = SetupFloorPortion(floorAllocation);
      floors[i].name = "floor" + (i + 1);
    }

    return floors;
  }
  
  
  void SetupFloorsAndDoors(float availableSpace) {

    var floors = GenerateFloorObjects(availableSpace);
    _leftSide = floors[0];
    _rightSide = floors[1];
    
    var parent = transform;

    var leftSideTransformPosition = _leftSide.transform.position;
    var leftSideLocalScale = _leftSide.transform.localScale;
    leftSideTransformPosition = new Vector3(
      leftSideTransformPosition.x + (leftSideLocalScale.x / 2),
      leftSideTransformPosition.y,
      leftSideTransformPosition.z
    );
    _leftSide.transform.position = leftSideTransformPosition;

    var parentPosition = parent.position;
    var rightSideOffset = new Vector3(
      leftSideTransformPosition.x + leftSideLocalScale.x + _doorSize + _rightSide.transform.position.x,
      parentPosition.y,
      parentPosition.z
    );
    _rightSide.transform.position = rightSideOffset;
  }

  void SetupWalls() {
    var leftWall = SetupWall();
    leftWall.name = "leftWall";
    var rightWall = SetupWall();
    rightWall.name = "rightWall";

    var parent = transform;
    var transformPosition = parent.position;
    rightWall.transform.position = new Vector3(
      _rightSide.transform.position.x + (_rightSide.transform.localScale.x / 2),
      rightWall.transform.position.y,
      transformPosition.z
    );
    
  }
}