using System.Collections.Generic;
using UnityEngine;

namespace BlackPad.DropCube.Level
{
  public class Room : MonoBehaviour
  {
    public delegate void ExitRoom();
    public static event ExitRoom OnRoomExit;

    public delegate void EnterRoom();

    public static event EnterRoom OnRoomEnter;

    public Door DoorComponent { get; private set; }
    bool isClosedDoor;
    Switch _switch;
    Vector3 _roomScale;
    List<Color> _colorPalette;
    Points _points;

    bool CheckIsDoorClosed()
    {
      return isClosedDoor = Random.Range(0, 100) >= 50;
    }

    public Room Initialize(
      Vector3 roomScale,
      List<Color> colorPalette,
      Door door,
      Switch switchComponent,
      Points points
    )
    {
      _roomScale = roomScale;
      _colorPalette = colorPalette;
      DoorComponent = door;
      _switch = switchComponent;
      _points = points;
      
      gameObject
        .AddComponent<BoxCollider>()
        .isTrigger = true;
      
      return this;
    }

    void OnTriggerEnter(Collider other)
    {
      OnRoomEnter?.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
      OnRoomExit?.Invoke();
    }
  }
}