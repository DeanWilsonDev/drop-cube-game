using BlackPad.Core.Utilities;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Wall
{
    public class WallsFactory
    {
        Color _color;

        readonly LevelObjectBuilder<> _wallLevelObjectBuilder;
        readonly WallGenerator _wallGenerator;
        Vector3 _rightWallPosition;
        
        public WallsFactory()
        {
            _wallLevelObjectBuilder = new LevelObjectBuilder<>();
            _wallGenerator = new WallGenerator();
        }
        
        
        public WallsFactory Initialize(
            Component parent,
            float roomHeight,
            float roomWidth,
            Color color
        )
        {
            _color = color;

            _wallGenerator.InitializeGenerator(
                parent,
                roomHeight,
                roomWidth
            );

            _wallLevelObjectBuilder.Initialize(
                _wallGenerator,
                null
            );

            _rightWallPosition = new Vector3(
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
            
            return this;
        }

        public Wall Build()
        {
            return _wallLevelObjectBuilder
                .SetPosition(_rightWallPosition)
                .SetScale()
                .SetColor(_color)
                .GetProduct();
        }
    }
}