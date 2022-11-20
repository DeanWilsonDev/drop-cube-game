using System;
using BlackPad.Core.Utilities;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Wall
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
        
        Wall BuildLeftWall(
            Component parent,
            Color color
        )
        {
            var localScale = parent.transform.localScale;
            
            var rightWallScaleScale = new Vector3(
                0.1f,
                1,
                1
            );
            

            return _wallLevelObjectBuilder.Initialize(
                    LeftWallName,
                    parent,
                    parent.transform.position,
                    rightWallScaleScale,
                    null,
                    color
                )
                .GeneratePrimitiveObject()
                .AddComponent()
                .SetPosition()
                .SetScale()
                .SetColor()
                .GetProduct();
        }

        Wall BuildRightWall(
            Component parent,
            Color color
            )
        {
            var transform = parent.transform;
            var position = transform.position;
            var localScale = transform.localScale;
            
            var rightWallPosition = new Vector3(
                localScale.x / 2,
                localScale.y / 2,
                2.5f
            );
            
            var rightWallScale = new Vector3(
                0.1f,
                1,
                1
            );

            return _wallLevelObjectBuilder.Initialize(
                    RightWallName,
                    parent,
                    rightWallPosition,
                    rightWallScale,
                    null,
                    color
                ) 
                .GeneratePrimitiveObject()
                .AddComponent()
                .SetPosition()
                .SetScale()
                .SetColor()
                .GetProduct();
        }
        
        BackWall BuildBackWall(
            Component parent,
            Color color
        )
        {
            var transform = parent.transform;
            var position = transform.position;
            var localScale = transform.localScale;
            
            var backWallPosition = new Vector3(
                0,
                0,
                localScale.z / 2
            );
            
            var backWallScale = new Vector3(
                1,
                1,
                0.1f
            );
            
            return _backWallLevelObjectBuilder.Initialize(
                    BackWallName,
                    parent,
                    backWallPosition,
                    backWallScale,
                    null,
                    color
                ) 
                .GeneratePrimitiveObject()
                .AddComponent()
                .SetPosition()
                .SetScale()
                .SetColor()
                .GetProduct();
        }
    }
}