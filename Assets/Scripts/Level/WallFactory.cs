using System;
using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class WallsFactory
    {

        readonly LevelObjectBuilder<Wall> _wallLevelObjectBuilder;
        readonly LevelObjectBuilder<BackWall> _backWallLevelObjectBuilder;
        const string LeftWallName = "Left Wall";
        const string RightWallName = "Right Wall";
        const string BackWallName = "Back Wall";
        
        public WallsFactory()
        {
            _wallLevelObjectBuilder = new LevelObjectBuilder<Wall>();
            _backWallLevelObjectBuilder = new LevelObjectBuilder<BackWall>();
        }


        public Tuple<Wall, Wall, BackWall> Build(
            Component parent,
            Color color
        )
        {
            return new Tuple<Wall, Wall, BackWall>(
                BuildLeftWall(parent, color),
                BuildRightWall(parent, color),
                BuildBackWall(parent, color)
            );
        }

        static Wall BuildLeftWall(
            Component parent,
            Color color
        )
        {
            var parentTransform = parent.transform;
            var parentScale = parentTransform.localScale;

            var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.gameObject.name = LeftWallName;
            wall.transform.parent = parentTransform;
            var parentTransformPosition = parentTransform.position;
            
            wall.transform.position = new Vector3(
                - parentScale.x / 2,
                parentTransformPosition.y,
                parentTransformPosition.z
            );
            wall.transform.localScale = new Vector3(
                0.05f,
                1,
                1.1f
            );
            ColorAssigner.AssignColor(wall, color);

            return wall.AddComponent<Wall>();
        }

        static Wall BuildRightWall(
            Component parent,
            Color color
        )
        {
            var parentTransform = parent.transform;
            var parentScale = parentTransform.localScale;

            var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.gameObject.name = RightWallName;
            wall.transform.parent = parentTransform;
            var parentTransformPosition = parentTransform.position;
            
            wall.transform.position = new Vector3(
                parentScale.x / 2,
                parentTransformPosition.y,
                parentTransformPosition.z
            );
            wall.transform.localScale = new Vector3(
                0.05f,
                1,
                1.1f
            );
            ColorAssigner.AssignColor(wall, color);

            return wall.AddComponent<Wall>();
        }

        static BackWall BuildBackWall(
            Component parent,
            Color color
        )
        {
            var parentTransform = parent.transform;
            var parentTransformPosition = parentTransform.position;

            var backWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            backWall.gameObject.name = BackWallName;
            backWall.transform.parent = parentTransform;
            backWall.transform.position = new Vector3(
                parentTransformPosition.x,
                parentTransformPosition.y,
                parentTransform.localScale.z / 2
            );
            backWall.transform.localScale = new Vector3(
                1,
                1,
                0.1f
            );
            ColorAssigner.AssignColor(backWall, color);

            return backWall.AddComponent<BackWall>();
        }
    }
}