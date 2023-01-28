using System;
using System.Collections.Generic;
using BlackPad.Core.Utilities;
using BlackPad.DropCube.Data;
using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class LevelManager : MonoBehaviour
    {

        [Header(
            "Room Management"
        )]
        [SerializeField]
        IntVariable _startingRoomAmount;

        public List<Room> _spawnedRooms;
        
        [SerializeField] ColorPalette _colorPalette;
        int _roomNumber = 1;


        [Header(
            "Room"
        )]
        [SerializeField]
        Vector3Variable _roomScale;
        
        [Header(
            "Door"
        )]
        [SerializeField]
        FloatVariable _doorSize;

        bool _isDoorClosed;
        [SerializeField] FloatVariable _doorSpawnThreshold;
        [SerializeField] GameObjectVariable _doorPrefab;

        [Header(
            "Switch"
        )]
        [SerializeField]
        GameObjectVariable _switchPrefab;

        FloorFactory _floorFactory;
        SwitchFactory _switchFactory;
        DoorFactory _doorFactory;
        RoomFactory _roomFactory;
        WallsFactory _wallsFactory;

        // Start is called before the first frame update
        void Start()
        {
            _spawnedRooms = new List<Room>();

            _floorFactory = new FloorFactory();
            _switchFactory = new SwitchFactory();
            _doorFactory = new DoorFactory();
            _wallsFactory = new WallsFactory();
            _roomFactory = new RoomFactory();

            for (var i = 0; i < _startingRoomAmount.value; i++)
            {
                SpawnNewRoom();
            }
        }

        void OnEnable()
        {
            Room.OnRoomExit += SpawnNewRoom;
        }

        void OnDisable()
        {
            Room.OnRoomExit -= SpawnNewRoom;
        }

        void SpawnNewRoom()
        {

            RemoveOldestRoom();
            
            Door doorComponent = null;
            Switch switchComponent = null;

            
            _isDoorClosed =
                Utilities.DetermineIfRandomlySelected(
                    _doorSpawnThreshold
                        .value
                );
                
            var roomObject = BuildRoom();

                
            var floorComponent = BuildFloor(
                roomObject
            );

            var pointsComponent = BuildRoomPoints(roomObject, 2500);
                
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
                
            roomObject.GetComponent<Room>()
                .Initialize(
                    _roomScale.value,
                    _colorPalette.value,
                    doorComponent,
                    switchComponent,
                    pointsComponent);
            _roomNumber++;
            
            _spawnedRooms.Add(roomObject);
        }

        void RemoveOldestRoom()
        {
            if (_spawnedRooms.Count < _startingRoomAmount.value * 2) return;
            var roomToDestroy = _spawnedRooms[0];
            Destroy(roomToDestroy.gameObject);
            _spawnedRooms.Remove(_spawnedRooms[0]);
        }
        

        Room BuildRoom() =>
            _roomFactory
                .Build(
                    transform,
                    _roomScale.value,
                    _roomNumber
                );

        Door BuildDoor(Component parentComponent, Floor floorComponent) =>
            _doorFactory
                .Build(
                    parentComponent,
                    floorComponent,
                    _doorSize.value,
                    _doorPrefab.value,
                    _colorPalette.value[3]
                );

        Switch BuildSwitch(Component parentComponent, Floor floorComponent) =>
            _switchFactory
                .Build(
                    parentComponent,
                    floorComponent,
                    _switchPrefab.value,
                    _colorPalette.value[4]
                );

        Floor BuildFloor(Component parentComponent)
        {
            return _floorFactory
                .Build(
                    parentComponent,
                    _doorSize.value,
                    _colorPalette.value[1]
                );
            
        }

        Tuple<Wall, Wall, BackWall> BuildWalls(Component parentComponent) =>
            _wallsFactory
                .Build(
                    parentComponent,
                    _colorPalette.value[0]
                );

        Points BuildRoomPoints(Component parentComponent, int roomScoreValue) =>
            PointsFactory.Build(parentComponent, roomScoreValue);
    }
}