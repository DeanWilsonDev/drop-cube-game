using System;
using System.Collections.Generic;
using System.Linq;
using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlackPad.DropCube.Level.Room.Floor
{
    public class FloorFactory
    {
        readonly LevelObjectBuilder<Floor> _floorLevelObjectBuilder;
        
        Floor _floor;
        Component _parent;
        Transform _floorParent;
        GameObject _leftSideFloor;
        GameObject _rightSideFloor;
        float _roomWidth;
        
        float _doorSize;
        const float MinOffset = 0;
        const float MaxOffset = 3;

        const string LeftFloorGameObjectName = "Left Floor";
        const string RightFloorGameObjectName = "Right Floor";
        const string FloorGameObjectName = "Floor";


        public FloorFactory()
        {
            _floorLevelObjectBuilder = new LevelObjectBuilder<Floor>();
        }

        public Floor Build(
            Component parent,
            float doorSize,
            Color color
        )
        {
            _doorSize = doorSize;
            var parentTransform = parent.transform;
            _parent = parent;

            _roomWidth = _parent.transform.localScale.x;
            
            _floorParent = new GameObject
            {
                name = FloorGameObjectName,
                transform =
                {
                    position = Vector3.zero,
                    localScale = Vector3.one,
                    parent = parentTransform
                }
            }.transform;

            _floorParent.gameObject.AddComponent<Floor>();


            _leftSideFloor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _rightSideFloor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var floorObjects = new List<GameObject>
            {
                _leftSideFloor,
                _rightSideFloor
            };
            SetFloorScale(floorObjects);
            
            _leftSideFloor.gameObject.name = LeftFloorGameObjectName;
            _leftSideFloor.transform.parent = parentTransform;
            _leftSideFloor.transform.position =
                GetLeftSideTransformPosition();
            ColorAssigner.AssignColor(_leftSideFloor, color);

            _rightSideFloor.gameObject.name = RightFloorGameObjectName;
            _rightSideFloor.transform.parent = parentTransform;
            _rightSideFloor.transform.position =
                GetRightSideTransformPosition();
            ColorAssigner.AssignColor(_rightSideFloor, color);
            
            // SetFloorParentAndReference(floorObjects);

            return _floorParent.GetComponent<Floor>();
        }

        Vector3 FindRoomOriginPoint()
        {
            var parentTransform = _parent.transform;
            var parentTransformLocalScale = parentTransform.localScale;
            var parentTransformPosition = parentTransform.position;
            
            return new Vector3(
                parentTransformPosition.x - parentTransformLocalScale.x / 2,
                parentTransformPosition.y - parentTransformLocalScale.y,
                parentTransformPosition.z
            );
        }

        float[] SelectDoorSide()
        {
            var isSelected = Utilities.DetermineIfRandomlySelected(50);
            var doorFlags = new[]
            {
                isSelected,
                !isSelected
            };
            return ApplyDoorAllocation(doorFlags);
        }

        float[] ApplyDoorAllocation(IReadOnlyList<bool> doorFlags)
        {
            var floorAllocation = new float[2];
            for (var i = 0; i < 2; i++)
            {
                floorAllocation[i] = _roomWidth / 2;
                floorAllocation[i] -=
                    Utilities.GetValueIfTrue(doorFlags[i], _doorSize);
            }

            return floorAllocation;
        }

        static float[] ApplyOffset(float offset, IReadOnlyList<bool> offsetFlags,
            float[] floorAllocation)
        {
            for (var i = 0; i < 2; i++)
            {
                floorAllocation[i] = offsetFlags[i]
                    ? floorAllocation[i] - offset
                    : floorAllocation[i] + offset;
            }

            return floorAllocation;
        }

        float[] SelectOffsetSide(float[] floorAllocation)
        {
            var isSelected = Utilities.DetermineIfRandomlySelected(50);
            var offsetFlags = new[]
            {
                isSelected,
                !isSelected
            };
            var offset = Random.Range(MinOffset, _roomWidth / MaxOffset);
            return ApplyOffset(offset, offsetFlags, floorAllocation);
        }

        void SetFloorScale(IReadOnlyList<GameObject> floorObjects)
        {
            var floorAllocation = SelectDoorSide();
            var offsetFloorAllocation = SelectOffsetSide(floorAllocation);

            for (var i = 0; i < floorObjects.Count; i++)
            {
                Utilities.SetGameObjectGlobalScale(
                    floorObjects[i], 
                    new Vector3(
                        offsetFloorAllocation[i], 
                        1,
                        _parent.transform.localScale.z
                        )
                    );
            }
        }

        Vector3 GetLeftSideTransformPosition()
        {
            var position = FindRoomOriginPoint();
            return 
                   new Vector3(
                    position.x + 
                        _leftSideFloor.transform.localScale.x / 2,
                    position.y,
                    position.z
            );
        }

        Vector3 GetRightSideTransformPosition() {
            var position = FindRoomOriginPoint();
            var leftSideWidth = _leftSideFloor.transform.localScale.x;
            var rightSideWidth = _rightSideFloor.transform.localScale.x;
            Debug.Log(position.x + 
                      rightSideWidth / 2 +
                      leftSideWidth + 
                      _doorSize);
            return new Vector3(
                position.x + 
                    rightSideWidth / 2 +
                    leftSideWidth + 
                    _doorSize,
                position.y,
                position.z
            );
        }
        
        void SetFloorParentAndReference(List<GameObject> floorObjects)
        {
            foreach (var floorObject in floorObjects)
            {
                floorObject.transform.parent = _floorParent.transform;
                _floorParent
                    .GetComponent<Floor>()
                    .floorGameObjects
                    .Add(floorObject);
            }
        }
    }
}