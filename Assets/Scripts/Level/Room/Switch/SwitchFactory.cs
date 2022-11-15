using UnityEngine;
using BlackPad.DropCube.Level.Room.Floor;

namespace BlackPad.DropCube.Level.Room.Switch
{
    public class SwitchFactory
    {
        Color _color;

        readonly LevelObjectBuilder<SwitchGenerator, Switch> _switchBuilder;
        readonly SwitchGenerator _switchGenerator;

        public SwitchFactory()
        {
            _switchBuilder = new LevelObjectBuilder<SwitchGenerator, Switch>();
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


            _switchBuilder.Initialize(
                _switchGenerator,
                prefab
            );

            return this;
        }

        public Switch Build() =>
            _switchBuilder
                .SetupPrefab()
                .SetPosition()
                .SetColor(_color)
                .GetProduct();
    }
}