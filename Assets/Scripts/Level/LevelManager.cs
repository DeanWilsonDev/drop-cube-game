using BlackPad.Core.Utilities;
using BlackPad.DropCube.Data;
using BlackPad.DropCube.Level.Room;
using BlackPad.DropCube.Level.Room.Door;
using BlackPad.DropCube.Level.Room.Floor;
using BlackPad.DropCube.Level.Room.Switch;
using BlackPad.DropCube.Level.Room.Wall;
using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class LevelManager : MonoBehaviour
    {
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
        [SerializeField]
        FloatVariable roomHeight;

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

        FloorFactory _floorFactory;
        SwitchFactory _switchFactory;
        DoorFactory _doorFactory;
        RoomFactory _roomFactory;
        WallsFactory _wallsFactory;

        // Start is called before the first frame update
        void Start()
        {
            Door doorComponent = null;
            Switch switchComponent = null;

            _floorFactory = new FloorFactory();
            _switchFactory = new SwitchFactory();
            _doorFactory = new DoorFactory();
            _wallsFactory = new WallsFactory();
            _roomFactory = new RoomFactory();

            for (var i = 0; i < startingRoomAmount.value; i++)
            {
                isDoorClosed =
                    Utilities.DetermineIfRandomlySelected(
                        doorSpawnThreshold
                        .value
                        );
                
                var roomObject = BuildRoom();
                
                var floorComponent = BuildFloor(
                    roomObject
                );
                
                if (isDoorClosed)
                {
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
                
                roomObject.GetComponent<Room.Room>()
                    .Initialize(
                        roomWidth.value,
                        roomHeight.value,
                        colorPalette.value,
                        doorComponent,
                        switchComponent);
                roomNumber++;
            }
        }

        Room.Room BuildRoom() =>
            _roomFactory
                .Initialize(
                    transform,
                    roomHeight.value,
                    roomNumber
                )
                .Build();

        Door BuildDoor(Component parentComponent, Floor floorComponent) =>
            _doorFactory
                .Initialize(
                    parentComponent,
                    floorComponent,
                    doorSize.value,
                    doorPrefab.value,
                    colorPalette.value[3]
                )
                .Build();

        Switch BuildSwitch(Component parentComponent, Floor floorComponent) =>
            _switchFactory
                .Initialize(
                    parentComponent,
                    floorComponent,
                    switchPrefab.value,
                    colorPalette.value[4]
                )
                .Build();

        Floor BuildFloor(Component parentComponent) =>
            _floorFactory
                .Initialize(
                    parentComponent,
                    roomWidth.value,
                    doorSize.value,
                    colorPalette.value[1]
                )
                .Build();

        Wall BuildWalls(Component parentComponent) =>
            _wallsFactory
                .Initialize(
                    parentComponent,
                    roomHeight.value,
                    roomWidth.value,
                    colorPalette.value[0]
                )
                .Build();
    }
}