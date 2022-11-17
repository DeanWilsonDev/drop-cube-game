using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Floor
{
    public class FloorFactory
    {
        Color _color;
        readonly LevelObjectBuilder<> _floorLevelObjectBuilder;
        readonly FloorGenerator _floorGenerator;

        public FloorFactory()
        {
            _floorLevelObjectBuilder = new LevelObjectBuilder<>();
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

            _floorLevelObjectBuilder.Initialize(
                floorGenerator,
                null
            );

            return this;
        }

        public Floor Build() =>
            _floorLevelObjectBuilder
                .SetupPrefab()
                .SetPosition()
                .SetColor(_color)
                .GetProduct();
    }
}