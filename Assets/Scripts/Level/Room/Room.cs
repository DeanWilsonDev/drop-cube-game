using System.Collections.Generic;
using UnityEngine;


namespace BlackPad.DropCube.Level.Room {

  public class Room : MonoBehaviour {
    
    // Door Variables
    [Header("Door Variables")]
    [SerializeField] Door.Door doorComponent;
    public Door.Door DoorComponent => doorComponent;
    [SerializeField] GameObject doorPrefab;
    [SerializeField] float doorSize;
    bool isClosedDoor;
    
    [Header("Switch")]
    [SerializeField] GameObject switchPrefab;
    [SerializeField] Switch.Switch switchComponent;

    [Header("Room Variables")] [SerializeField]
    Vector3 roomScale;
    GameObject _leftSide;
    GameObject _rightSide;
    [SerializeField] Wall.Wall walls;
    [SerializeField] Wall.BackWall backWall;
    [SerializeField] Floor.Floor floor;
    [SerializeField] List<Color> colorPalette;

    bool CheckIsDoorClosed() {
      return isClosedDoor = Random.Range(0, 100) >= 50;
    }

    public Room Initialize(
      Vector3 roomScale,
      List<Color> colorPalette,
      Door.Door doorComponent,
      Switch.Switch switchComponent
      ) {
      this.roomScale = roomScale;
      this.colorPalette = colorPalette;
      this.doorComponent = doorComponent;
      this.switchComponent = switchComponent;
      gameObject.AddComponent<BoxCollider>().isTrigger = true;
      return this;
    }
  }
}