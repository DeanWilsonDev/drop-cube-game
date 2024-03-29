using BlackPad.Core.Utilities;
using BlackPad.DropCube.Data;
using UnityEngine;

namespace BlackPad.DropCube.Level {

  public class LevelManager : MonoBehaviour {

    [Header(
      "Room Management"
    )]
    [SerializeField]
    IntVariable startingRoomAmount;

    [SerializeField] ColorPalette colorPalette;
    int roomNumber = 1;

    [Header(
      "Room"
    )]
    [SerializeField] FloatVariable roomHeight;
    [SerializeField] FloatVariable roomWidth;

    public float RoomWidth => roomWidth.value;

    [Header(
      "Door"
    )]
    [SerializeField]
    FloatVariable doorSize;

    bool isDoorClosed = false;
    [SerializeField] FloatVariable doorSpawnThreshold;
    [SerializeField] GameObjectVariable doorPrefab;

    [Header(
      "Switch"
    )]
    [SerializeField]
    GameObjectVariable switchPrefab;

    // Start is called before the first frame update
    void Start() {
      Door doorComponent = null;
      Switch switchComponent = null;
      
      for (var i = 0; i < startingRoomAmount.value; i++) {
        isDoorClosed = Utilities.DetermineIfRandomlySelected(doorSpawnThreshold.value);
        var roomObject = BuildRoom();
        var floorComponent = BuildFloor(
          roomObject
        );
        if (isDoorClosed) {
          doorComponent = BuildDoor(
            roomObject,
            floorComponent
          );
          switchComponent = BuildSwitch(
            roomObject,
            floorComponent
          );
        }
        var wallsComponent = BuildWalls(
          roomObject
        );
        roomObject.GetComponent<Room>()
          .Initialize(
            roomWidth.value,
            roomHeight.value,
            colorPalette.value,
            doorComponent,
            switchComponent);
        roomNumber++;
      }
    }

    Room BuildRoom() => new RoomFactory(
      transform,
      roomHeight.value,
      roomNumber
    ).Build();

    Door BuildDoor(Component parentComponent, Floor floorComponent) => new DoorFactory(
      parentComponent,
      floorComponent,
      doorSize.value,
      doorPrefab.value,
      colorPalette.value[3]
    ).Build();

    Switch BuildSwitch(Component parentComponent, Floor floorComponent) => new SwitchFactory(
      parentComponent,
      floorComponent,
      switchPrefab.value,
      colorPalette.value[4]
    ).Build();

    Floor BuildFloor(Component parentComponent) => new FloorFactory(
        parentComponent,
        roomWidth.value,
        doorSize.value,
        colorPalette.value[1]
      )
      .Build();

    Wall BuildWalls(Component parentComponent) => new WallsFactory(
      parentComponent,
      roomHeight.value,
      roomWidth.value,
      colorPalette.value[0]
    ).Build();

  }
}