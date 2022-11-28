using System;
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
        int _roomNumber = 1;

        [Header(
            "Room"
        )]
        [SerializeField]
        Vector3Variable roomScale;

        [Header(
            "Door"
        )]
        [SerializeField]
        FloatVariable doorSize;

        bool _isDoorClosed;
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


            _floorFactory = new FloorFactory();
            _switchFactory = new SwitchFactory();
            _doorFactory = new DoorFactory();
            _wallsFactory = new WallsFactory();
            _roomFactory = new RoomFactory();

            for (var i = 0; i < startingRoomAmount.value; i++)
            {
                SpawnNewRoom();
                
            }
        }

        void OnEnable()
        {
            Room.Room.OnRoomExit += SpawnNewRoom;
        }

        void OnDisable()
        {
            Room.Room.OnRoomExit -= SpawnNewRoom;
        }

        void SpawnNewRoom()
        {
            Debug.Log("New Room Spawned");
            Door doorComponent = null;
            Switch switchComponent = null;
            
            _isDoorClosed =
                Utilities.DetermineIfRandomlySelected(
                    doorSpawnThreshold
                        .value
                );
                
            var roomObject = BuildRoom();

                
            var floorComponent = BuildFloor(
                roomObject
            );
                
            if (_isDoorClosed)
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

            var (leftWall, rightWall, backWall) = BuildWalls(
                roomObject
            );
                
            roomObject.GetComponent<Room.Room>()
                .Initialize(
                    roomScale.value,
                    colorPalette.value,
                    doorComponent,
                    switchComponent);
            _roomNumber++;
        }

        Room.Room BuildRoom() =>
            _roomFactory
                .Build(
                    transform,
                    roomScale.value,
                    _roomNumber
                    );

        Door BuildDoor(Component parentComponent, Floor floorComponent) =>
            _doorFactory
                .Build(
                    parentComponent,
                    floorComponent,
                    doorSize.value,
                    doorPrefab.value,
                    colorPalette.value[3]
                );

        Switch BuildSwitch(Component parentComponent, Floor floorComponent) =>
            _switchFactory
                .Build(
                    parentComponent,
                    floorComponent,
                    switchPrefab.value,
                    colorPalette.value[4]
                );

        Floor BuildFloor(Component parentComponent)
        {
            return _floorFactory
                .Build(
                    parentComponent,
                    doorSize.value,
                    colorPalette.value[1]
                );
            
        }

        Tuple<Wall, Wall, BackWall> BuildWalls(Component parentComponent) =>
            _wallsFactory
                .Build(
                    parentComponent,
                    colorPalette.value[0]
                );
    }
}