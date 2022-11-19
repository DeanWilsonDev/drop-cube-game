using BlackPad.Core.Utilities;
using UnityEngine;
using BlackPad.DropCube.Level.Room.Floor;

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
        
        public Switch Initialize(
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
                .SetupPrefab()
                .SetPosition()
                .SetColor()
                .GetProduct();
        }

        static Vector3 SwitchPosition(GameObject floorObject)
            => floorObject
                   .transform
                   .position
               + new Vector3(0, 1, 0);

        Vector3 GetSwitchPosition() {
            
            var isLargerFloorObject =
                Utilities.GameObjectWidth(_floor.floorGameObjects[0])
                >= Utilities.GameObjectWidth(_floor.floorGameObjects[1]);
            
            return isLargerFloorObject
                ? SwitchPosition(_floor.floorGameObjects[0])
                : SwitchPosition(_floor.floorGameObjects[1]);
        }

    }
}