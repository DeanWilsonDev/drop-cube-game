using System.Collections.Generic;
using BlackPad.DropCube.Data;
using UnityEngine;

namespace BlackPad.DropCube.Level {

  public class Room : MonoBehaviour {
    
    // Door Variables
    [Header("Door Variables")]
    [SerializeField] Door doorComponent;
    [SerializeField] GameObject doorPrefab;
    [SerializeField] float doorSize;
    bool isClosedDoor;
    
    [Header("Switch")]
    [SerializeField] GameObject switchPrefab;
    [SerializeField] Switch switchComponent;
    
    [Header("Room Variables")]
    [SerializeField] float roomHeight ;
    [SerializeField] float roomWidth;
    GameObject leftSide;
    GameObject rightSide;
    [SerializeField] Wall walls;
    [SerializeField] BackWall backWall;
    [SerializeField] Floor floor;
    [SerializeField] List<Color> colorPalette;

    bool CheckIsDoorClosed() {
      return isClosedDoor = Random.Range(0, 100) >= 50;
    }

    public Room SetupRoom(float roomHeight, float roomWidth, List<Color> colorPalette) {
      this.roomHeight = roomHeight;
      this.roomWidth = roomWidth;
      this.colorPalette = colorPalette;
      return this;
    }
  }
}