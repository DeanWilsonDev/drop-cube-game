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
            float roomHeight,
            float roomWidth,
            Color color
        )
        {
            return new Tuple<Wall, Wall, BackWall>(
                BuildLeftWall(parent, roomHeight, roomWidth, color),
                BuildRightWall(parent, roomHeight, roomWidth, color),
                BuildBackWall(parent, roomHeight, roomWidth, color)
                );
        }
        
        Wall BuildLeftWall(
            Component parent,
            float roomHeight,
            float roomWidth,
            Color color
        )
        {

            var rightWallScaleScale = new Vector3(
                1,
                roomHeight,
                5
            );
            

            return _wallLevelObjectBuilder.Initialize(
                    LeftWallName,
                    parent,
                    null,
                    rightWallScaleScale,
                    GameObject.CreatePrimitive(
                        PrimitiveType.Cube
                    ),
                    color
                ) 
                .SetPosition()
                .SetScale()
                .SetColor()
                .GetProduct();
        }

        Wall BuildRightWall(
            Component parent,
            float roomHeight,
            float roomWidth,
            Color color
            )
        {
            var rightWallPosition = new Vector3(
                Utilities.GameObjectTransformPosition(
                        parent.gameObject
                    )
                    .x
                + roomWidth / 2,
                Utilities.GameObjectTransformPosition(
                        parent.gameObject
                    )
                    .y
                + roomHeight / 2,
                Utilities.GameObjectTransformPosition(
                        parent.gameObject
                    )
                    .z
                + 2.5f
            );
            
            var rightWallScale = new Vector3(
                1,
                roomHeight,
                5
            );
            

            return _wallLevelObjectBuilder.Initialize(
                    RightWallName,
                    parent,
                    rightWallPosition,
                    rightWallScale,
                    GameObject.CreatePrimitive(
                        PrimitiveType.Cube
                    ),
                    color
                ) 
                .SetPosition()
                .SetScale()
                .SetColor()
                .GetProduct();
        }
        
        BackWall BuildBackWall(
            Component parent,
            float roomHeight,
            float roomWidth,
            Color color
        )
        {
            var backWallPosition = new Vector3(
                Utilities.GameObjectTransformPosition(
                        parent.gameObject
                    )
                    .x
                + roomWidth / 2,
                Utilities.GameObjectTransformPosition(
                        parent.gameObject
                    )
                    .y
                + roomHeight / 2,
                Utilities.GameObjectTransformPosition(
                        parent.gameObject
                    )
                    .z
                + 2.5f
            );
            
            var backWallScale = new Vector3(
                roomWidth,
                roomHeight,
                1
            );
            

            return _backWallLevelObjectBuilder.Initialize(
                    BackWallName,
                    parent,
                    backWallPosition,
                    backWallScale,
                    GameObject.CreatePrimitive(
                        PrimitiveType.Cube
                    ),
                    color
                ) 
                .SetPosition()
                .SetScale()
                .SetColor()
                .GetProduct();
        }
    }
}