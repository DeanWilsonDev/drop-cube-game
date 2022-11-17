using UnityEngine;
using BlackPad.DropCube.Level.Room.Floor;

namespace BlackPad.DropCube.Level.Room.Switch
{
    public class SwitchFactory
    {
        Color _color;

        readonly LevelObjectBuilder<> _switchLevelObjectBuilder;
        readonly SwitchGenerator _switchGenerator;

        public SwitchFactory()
        {
            _switchLevelObjectBuilder = new LevelObjectBuilder<>();
            _switchGenerator = new SwitchGenerator();
        }


        public SwitchFactory Initialize(Component parent, Floor.Floor floor,
            GameObject prefab, Color color)
        {
            _color = color;

            _switchGenerator
                .InitializeGenerator(
                    parent,
                    floor
                );


            _switchLevelObjectBuilder.Initialize(
                _switchGenerator,
                prefab
            );

            return this;
        }

        public Switch Build() =>
            _switchLevelObjectBuilder
                .SetupPrefab()
                .SetPosition()
                .SetColor(_color)
                .GetProduct();
    }
}