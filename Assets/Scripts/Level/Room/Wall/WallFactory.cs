using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Wall
{
    public class WallsFactory
    {
        Color _color;

        readonly LevelObjectBuilder<WallGenerator, Wall> _wallBuilder;
        readonly WallGenerator _wallGenerator;
        
        public WallsFactory()
        {
            _wallBuilder = new LevelObjectBuilder<WallGenerator, Wall>();
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

            _wallBuilder.Initialize(
                _wallGenerator,
                null
            );
            
            return this;
        }

        public Wall Build()
        {
            return _wallBuilder
                .SetPosition()
                .SetColor(_color)
                .GetProduct();
        }
    }
}