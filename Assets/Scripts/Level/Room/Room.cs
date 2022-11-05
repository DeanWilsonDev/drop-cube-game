using System.Collections.Generic;
using UnityEngine;


namespace BlackPad.DropCube.Level.Room {

  public class Room : MonoBehaviour {
    
    // Door Variables
    [Header("Door Variables")]
    [SerializeField] Door.Door doorComponent;
    [SerializeField] GameObject doorPrefab;
    [SerializeField] float doorSize;
    bool isClosedDoor;
    
    [Header("Switch")]
    [SerializeField] GameObject switchPrefab;
    [SerializeField] Level.Switch switchComponent;
    
    [Header("Room Variables")]
    [SerializeField] float roomHeight ;
    [SerializeField] float roomWidth;
    GameObject leftSide;
    GameObject rightSide;
    [SerializeField] Wall.Wall walls;
    [SerializeField] Wall.BackWall backWall;
    [SerializeField] Floor.Floor floor;
    [SerializeField] List<Color> colorPalette;

    bool CheckIsDoorClosed() {
      return isClosedDoor = Random.Range(0, 100) >= 50;
    }

    public Room Initialize(float roomHeight, float roomWidth, List<Color> colorPalette, Door.Door doorComponent, Level.Switch switchComponent) {
      this.roomHeight = roomHeight;
      this.roomWidth = roomWidth;
      this.colorPalette = colorPalette;
      this.doorComponent = doorComponent;
      this.switchComponent = switchComponent;
      return this;
    }
  }
}