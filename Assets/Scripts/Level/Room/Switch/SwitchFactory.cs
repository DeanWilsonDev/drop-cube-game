using BlackPad.Core.Utilities;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Switch
{
    public class SwitchFactory
    {
        readonly LevelObjectBuilder<Switch> _switchLevelObjectBuilder;
        const string SwitchObjectName = "Switch";
        Floor.Floor _floor;

        public SwitchFactory()
        {
            _switchLevelObjectBuilder = new LevelObjectBuilder<Switch>();
        }

        public Switch Build(
            Component parent,
            Floor.Floor floor,
            GameObject prefab,
            Color color
        )
        {
            _floor = floor;

            return _switchLevelObjectBuilder.Initialize(
                    SwitchObjectName,
                    parent,
                    GetSwitchPosition(),
                    null,
                    prefab,
                    color
                )
                .GeneratePrefabObject()
                .AddComponent()
                .SetPosition()
                .SetColor()
                .GetProduct();
        }

        static Vector3 SwitchPosition(GameObject floorObject)
            => floorObject
                   .transform
                   .position
               + new Vector3(0, 1, 0);

        Vector3 GetSwitchPosition()
        {
            var isLargerFloorObject =
                Utilities.GameObjectWidth(_floor.floorGameObjects[0])
                >= Utilities.GameObjectWidth(_floor.floorGameObjects[1]);

            return isLargerFloorObject
                ? SwitchPosition(_floor.floorGameObjects[0])
                : SwitchPosition(_floor.floorGameObjects[1]);
        }
    }
}