using System.Collections.Generic;
using UnityEngine;

namespace BlackPad.DropCube.Level
{
  public class Room : MonoBehaviour
  {
    public delegate void ExitRoom();
    public static event ExitRoom OnRoomExit;
    
    
    
    public Door DoorComponent { get; private set; }
    bool isClosedDoor;
    Switch _switch;
    Vector3 _roomScale;
    List<Color> _colorPalette;
    Points _points;
    PointsManager _pointsManager;
    int _roomScoreValue;
    const string GameManagerTag = "GameManager";
    bool CheckIsDoorClosed()
    {
      return isClosedDoor = Random.Range(0, 100) >= 50;
    }

    public Room Initialize(
      Vector3 roomScale,
      int roomScoreValue,
      List<Color> colorPalette,
      Door door,
      Switch switchComponent,
      Points points
    )
    {
      _roomScale = roomScale;
      _roomScoreValue = roomScoreValue;
      _colorPalette = colorPalette;
      DoorComponent = door;
      _switch = switchComponent;
      _points = points;
      _pointsManager = GameObject.FindGameObjectWithTag(GameManagerTag).GetComponent<PointsManager>();
      
      var trigger = gameObject
        .AddComponent<BoxCollider>();

      trigger.isTrigger = true;
      trigger.size = Vector3.one *  0.9f;
      return this;
    }

    void OnTriggerEnter(Collider other)
    {
      _pointsManager.CurrentPoints += _roomScoreValue;
      _points.DisplayCurrentPointsValue(_pointsManager.CurrentPoints.ToString());
    }

    void OnTriggerExit(Collider other)
    {
      _points.DisplayCurrentPointsValue("");
      OnRoomExit?.Invoke();
    }
  }
}