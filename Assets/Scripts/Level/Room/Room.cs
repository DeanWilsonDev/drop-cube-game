using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace BlackPad.DropCube.Level.Room {

  public class Room : MonoBehaviour
  {
    public delegate void ExitRoom();
    public static event ExitRoom OnRoomExit;
    
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

    TextMesh _roomScoreText;
    int _roomScoreValue;
    void Start()
    {
      _roomScoreValue = 2500;
      _roomScoreText = new GameObject().AddComponent<TextMesh>();
      _roomScoreText.text = _roomScoreValue.ToString();
      _roomScoreText.fontSize = 100;
      var roomScoreTextTransform = _roomScoreText.transform;
      roomScoreTextTransform.parent = transform.parent;
      var position = transform.position;
      roomScoreTextTransform.position = new Vector3( 
        position.x - 10,
        position.y + 5,
        position.z + 3
      );
      roomScoreTextTransform.localScale = new Vector3(
        1, 
        1, 
        1 
        );
    }

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

    void OnTriggerExit(Collider other)
    {
      OnRoomExit?.Invoke();
    }
  }
}