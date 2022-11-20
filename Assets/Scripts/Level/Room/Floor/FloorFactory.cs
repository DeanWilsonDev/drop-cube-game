using System;
using System.Collections.Generic;
using BlackPad.Core.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlackPad.DropCube.Level.Room.Floor
{
    public class FloorFactory
    {
        readonly LevelObjectBuilder<Floor> _floorLevelObjectBuilder;
        
        Floor _floor;
        Transform _parent;
        GameObject _leftSideFloor;
        GameObject _rightSideFloor;
        
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
            
            _parent = new GameObject
            {
                name = FloorGameObjectName,
                transform =
                {
                    position = parentTransform.position,
                    parent = parentTransform
                }
            }.transform;

            _parent.gameObject.AddComponent<Floor>();
            
            var (
                    leftSideFloorScale, 
                    rightSideFloorScale
                    ) =
                GetFloorScaleVectors();


            _leftSideFloor = _floorLevelObjectBuilder.Initialize(
                    LeftFloorGameObjectName,
                    parent,
                    null,
                    null,
                    null,
                    color
          
                )
                .GeneratePrimitiveObject()
                .SetColor()
                .GetGeneratedObject();


            _rightSideFloor = _floorLevelObjectBuilder.Initialize(
                RightFloorGameObjectName,
                parent,
                null,
                null,
                null, 
                color
            )
                .GeneratePrimitiveObject()
                .SetColor()
                .GetGeneratedObject();

            var floorObjects = new List<GameObject>
            {
                _leftSideFloor,
                _rightSideFloor
            };
            
            _leftSideFloor.transform.position =
                GetLeftSideTransformPosition(leftSideFloorScale);

            _leftSideFloor.transform.position = leftSideFloorScale;

            _rightSideFloor.transform.position =
                GetRightSideTransformPosition(rightSideFloorScale);

            _rightSideFloor.transform.localScale = rightSideFloorScale;

            SetFloorParentAndReference(floorObjects);

            return _parent.GetComponent<Floor>();
        }


        Vector3 GetFloorPortionScale(float size) => new (size, 1, _parent.transform.localScale.z);

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
                floorAllocation[i] = 0.5f;
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
            var offset = Random.Range(MinOffset, _parent.transform.localScale.x / MaxOffset);
            return ApplyOffset(offset, offsetFlags, floorAllocation);
        }

        Tuple<Vector3, Vector3> GetFloorScaleVectors()
        {
            var floorScaleVectors = new List<Vector3>
            {
                Vector3.zero,
                Vector3.zero
            };
            
            var floorAllocation = SelectDoorSide();
            var offsetFloorAllocation = SelectOffsetSide(floorAllocation);
            
            for (var i = 0; i < floorScaleVectors.Count; i++)
            {
                floorScaleVectors[i] = GetFloorPortionScale(offsetFloorAllocation[i]);
            }

            return new Tuple<Vector3, Vector3>(
                floorScaleVectors[0], 
                floorScaleVectors[1]
            );;
        }

        Vector3 GetLeftSideTransformPosition(Vector3 leftSideScale)
        {
            var position = _parent.transform.position;
            return new Vector3(
                position.x + leftSideScale.x / 2,
                position.y,
                position.z
            );
        }

        Vector3 GetRightSideTransformPosition(Vector3 rightSideScale) {
            var position = _parent.transform.position;
            return new Vector3(
                rightSideScale.x
                + _doorSize
                + rightSideScale.x / 2,
                position.y,
                position.z
            );
        }
        
        void SetFloorParentAndReference(List<GameObject> floorObjects)
        {
            foreach (var floorObject in floorObjects)
            {
                floorObject.transform.parent = _parent.transform;
                _parent
                    .GetComponent<Floor>()
                    .floorGameObjects
                    .Add(floorObject);
            }
        }
    }
}