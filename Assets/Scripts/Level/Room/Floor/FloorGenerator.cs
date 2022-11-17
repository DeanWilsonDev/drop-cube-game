using System.Collections.Generic;
using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Floor
{
    public class FloorGenerator
        : Generator,
            IGenerator<Floor>
    {
        const float MIN_OFFSET = 0;
        const float MAX_OFFSET = 3;
        const string FLOOR_GAME_OBJECT_NAME = "Floor";
        const int LEFT_SIDE = 0;
        const int RIGHT_SIDE = 1;

        Floor _floor;
        GameObject[] _floorObjects;

        float _doorSize;
        float _roomWidth;

        public FloorGenerator InitializeGenerator(Component parent,
            float roomWidth, float doorSize)
        {
            Parent = parent;
            _roomWidth = roomWidth;
            _doorSize = doorSize;
            return this;
        }

        GameObject SetupFloorPortion(float size)
        {
            var floorSide = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var parentTransform = Parent.transform;
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

        static float[] ApplyOffset(float offset, bool[] offsetFlags,
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
            var offset = Random.Range(MIN_OFFSET, _roomWidth / MAX_OFFSET);
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
                    .name = FLOOR_GAME_OBJECT_NAME + (i + 1);
            }
        }

        Vector3 GetLeftSideTransformPosition() =>
            new(
                Utilities.GameObjectTransformPosition(_floorObjects[LEFT_SIDE])
                    .x
                + Utilities.GameObjectWidth(_floorObjects[LEFT_SIDE]) / 2,
                Utilities.GameObjectTransformPosition(_floorObjects[LEFT_SIDE])
                    .y,
                Utilities.GameObjectTransformPosition(_floorObjects[LEFT_SIDE])
                    .z
            );

        Vector3 GetRightSideTransformPosition()
        {
            var position = Parent.transform.position;
            return new Vector3(
                Utilities.FindOriginPoint(_floorObjects[LEFT_SIDE])
                + Utilities.GameObjectWidth(_floorObjects[LEFT_SIDE])
                + _doorSize
                + (Utilities.GameObjectWidth(_floorObjects[RIGHT_SIDE]) / 2),
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

        public Floor GetGeneratedFloor()
        {
            return _floor;
        }

        public void SetPosition()
        {
            _floorObjects[LEFT_SIDE]
                .transform.position = GetLeftSideTransformPosition();
            _floorObjects[RIGHT_SIDE]
                .transform.position = GetRightSideTransformPosition();
        }

        public Floor Generate()
        {
            _floor = this.Initialize<Floor>(FLOOR_GAME_OBJECT_NAME);
            return _floor;
        }

        public void SetupPrefab(GameObject prefab)
        {
            GenerateFloorObjects();
            SetFloorParentAndReference();
        }

        public void SetColor(Color color)
        {
            var colorId = Shader.PropertyToID(
                "_Color"
            );
            _floorObjects[0]
                .gameObject
                .GetComponent<Renderer>()
                .material
                .SetColor(
                    colorId,
                    color
                );
            _floorObjects[1]
                .gameObject
                .GetComponent<Renderer>()
                .material
                .SetColor(
                    colorId,
                    color
                );
        }
    }
}