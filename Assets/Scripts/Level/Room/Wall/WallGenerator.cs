using System;
using System.Collections.Generic;
using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Wall
{
    public class WallGenerator
        : Generator,
            IGenerator<Wall>
    {
        const string WallsParentObjectName = "Walls";
        const string LeftWallName = "Left Wall";
        const string RightWallName = "Right Wall";
        const string BackWallName = "Back Wall";

        GameObject _walls;
        float _roomHeight;
        Component _parent;
        float _roomWidth;
        BackWall _backWall;
        Wall _rightWall;
        Wall _leftWall;


        public WallGenerator InitializeGenerator(
            Component parent,
            float roomHeight,
            float roomWidth
        )
        {
            _parent = parent;
            _roomHeight = roomHeight;
            _roomWidth = roomWidth;
            
            return this;
        }

        BackWall GenerateBackWall()
        {
            var backWallObject = GameObject.CreatePrimitive(
                PrimitiveType.Cube
            );
            backWallObject.name = BackWallName;
            backWallObject.transform.localScale = new Vector3(
                _roomWidth,
                _roomHeight,
                1
            );
            backWallObject.transform.parent = _parent.transform;
            return backWallObject.AddComponent<BackWall>();
        }

        Wall GenerateWall(string wallObjectName)
        {
            var wall = GameObject.CreatePrimitive(
                PrimitiveType.Cube
            );
            wall.transform.localScale = new Vector3(
                1,
                _roomHeight,
                5
            );
            var parentTransform = _parent.transform;
            var parentPosition = parentTransform.position;
            wall.transform.position = new Vector3(
                parentPosition.x,
                parentPosition.y + (wall.transform.localScale.y / 2),
                parentPosition.z
            );
            wall.gameObject.name = wallObjectName;
            return wall.AddComponent<Wall>();
        }

        GameObject GenerateWalls()
        {
            this._leftWall = GenerateWall(
                LeftWallName
            );

            this._rightWall = GenerateWall(
                RightWallName
            );

            _backWall = GenerateBackWall();
            _walls = new GameObject
            {
                name = WallsParentObjectName
            };

            _leftWall.transform.parent = _walls.transform;
            _rightWall.transform.parent = _walls.transform;
            _backWall.transform.parent = _walls.transform;
            _walls.transform.parent = _parent.transform;

            var wallComponent = _walls.AddComponent<Wall>();

            wallComponent.wallGameObjects = new List<GameObject>
            {
                _leftWall.gameObject,
                _rightWall.gameObject,
                _backWall.gameObject
            };

            return _walls;
        }

        public Wall Initialize()
        {
            return GenerateWalls()
                .GetComponent<Wall>();
        }

        public void SetupPrefab(GameObject prefab)
        {
            throw new NotImplementedException();
        }

        public void SetPosition()
        {
            _rightWall.transform.position = new Vector3(
                Utilities.GameObjectTransformPosition(
                        _parent.gameObject
                    )
                    .x
                + _roomWidth,
                _rightWall.transform.position.y,
                Utilities.GameObjectTransformPosition(
                        _parent.gameObject
                    )
                    .z
            );

            _backWall.transform.position = new Vector3(
                Utilities.GameObjectTransformPosition(
                        _parent.gameObject
                    )
                    .x
                + _roomWidth / 2,
                Utilities.GameObjectTransformPosition(
                        _parent.gameObject
                    )
                    .y
                + _roomHeight / 2,
                Utilities.GameObjectTransformPosition(
                        _parent.gameObject
                    )
                    .z
                + 2.5f
            );
        }

        public void SetColor(Color color)
        {
            var colorId = Shader.PropertyToID(
                "_Color"
            );
            _backWall
                .gameObject
                .GetComponent<Renderer>()
                .material
                .SetColor(
                    colorId,
                    color
                );

            _leftWall
                .gameObject
                .GetComponent<Renderer>()
                .material
                .SetColor(
                    colorId,
                    color
                );

            _rightWall
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