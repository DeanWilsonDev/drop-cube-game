using System.Collections.Generic;
using BlackPad.Core.Utilities;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Floor
{
    public class FloorFactory
    {
        readonly LevelObjectBuilder<Floor> _floorLevelObjectBuilder;
        
        Floor _floor;
        GameObject[] _floorObjects;
        Component _parent;
        
        float _doorSize;
        float _roomWidth;
        const float MinOffset = 0;
        const float MaxOffset = 3;
        const string LeftFloorGameObjectName = "Left Floor";
        const string RightFloorGameObjectName = "Right Floor";

        const int LeftSide = 0;
        const int RightSide = 1;

        public FloorFactory()
        {
            _floorLevelObjectBuilder = new LevelObjectBuilder<Floor>();
        }

        public FloorFactory Initialize(
            Component parent,
            float roomWidth,
            float doorSize,
            Color color
        )
        {
            _parent = parent;
            
            
            
            
            var leftSideFloor = _floorLevelObjectBuilder.Initialize(
                    LeftFloorGameObjectName,
                    parent,
                    GetLeftSideTransformPosition(),
          
                )
                .SetupPrefab()
                .SetPosition()
                .SetColor()
                .GetProduct();
            
            var rightSideFloor = _floorLevelObjectBuilder.Initialize(
                RightFloorGameObjectName,
                parent,
                GetRightSideTransformPosition()
                )
            
            
        }
        
        GameObject SetupFloorPortion(float size)
        {
            var floorSide = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var parentTransform = _parent.transform;
            floorSide.transform.position = parentTransform.position;
            floorSide.transform.localScale = new Vector3(size, 1, 5);
            floorSide.transform.parent = parentTransform;
            return floorSide;
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

        void GenerateFloorObjects()
        {
            _floorObjects = new GameObject[2];
            var floorAllocation = SelectDoorSide();
            var offsetFloorAllocation = SelectOffsetSide(floorAllocation);
            for (var i = 0; i < 2; i++)
            {
                _floorObjects[i] = SetupFloorPortion(offsetFloorAllocation[i]);
                _floorObjects[i]
                    .name = FloorGameObjectName + (i + 1);
            }
        }

        Vector3 GetLeftSideTransformPosition() =>
            new(
                Utilities.GameObjectTransformPosition(_floorObjects[LeftSide])
                    .x
                + Utilities.GameObjectWidth(_floorObjects[LeftSide]) / 2,
                Utilities.GameObjectTransformPosition(_floorObjects[LeftSide])
                    .y,
                Utilities.GameObjectTransformPosition(_floorObjects[LeftSide])
                    .z
            );

        Vector3 GetRightSideTransformPosition()
        {
            var position = _parent.transform.position;
            return new Vector3(
                Utilities.FindOriginPoint(_floorObjects[LeftSide])
                + Utilities.GameObjectWidth(_floorObjects[LeftSide])
                + _doorSize
                + (Utilities.GameObjectWidth(_floorObjects[RightSide]) / 2),
                position.y,
                position.z
            );
        }

        void SetFloorParentAndReference()
        {
            foreach (var floorObject in _floorObjects)
            {
                floorObject.transform.parent = _floor.transform;
                _floor
                    .floorGameObjects
                    .Add(floorObject);
            }
        }
        

        public void SetPosition()
        {
            _floorObjects[LeftSide]
                .transform.position = 
            _floorObjects[RightSide]
                .transform.position = GetRightSideTransformPosition();
        }
        

        public void GetPrefab()
        {
            GenerateFloorObjects();
            SetFloorParentAndReference();
        }
    }
}