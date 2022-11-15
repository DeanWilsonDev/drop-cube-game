using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Floor
{
    public class FloorFactory
    {
        Color _color;
        readonly LevelObjectBuilder<FloorGenerator, Floor> _floorBuilder;
        readonly FloorGenerator _floorGenerator;

        public FloorFactory()
        {
            _floorBuilder = new LevelObjectBuilder<FloorGenerator, Floor>();
            _floorGenerator = new FloorGenerator();
        }

        public FloorFactory Initialize(
            Component parent,
            float roomWidth,
            float doorSize,
            Color color
        )
        {
            _color = color;

            var floorGenerator = _floorGenerator.InitializeGenerator(
                parent,
                roomWidth,
                doorSize
            );

            _floorBuilder.Initialize(
                floorGenerator,
                null
            );

            return this;
        }

        public Floor Build() =>
            _floorBuilder
                .SetupPrefab()
                .SetPosition()
                .SetColor(_color)
                .GetProduct();
    }
}