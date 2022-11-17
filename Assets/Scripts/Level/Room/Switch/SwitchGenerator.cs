using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Switch
{
    public class SwitchGenerator: Generator, IGenerator<Switch> {

        const string SwitchParentName = "Switch";
        Floor.Floor _floor;
        Switch _switchComponent;
        
        public SwitchGenerator InitializeGenerator(Component parent, Floor.Floor floor) {
            Parent = parent;
            _floor = floor;
            return this;
        }
        
        static Vector3 SwitchPosition(GameObject floorObject)
            => floorObject
                   .transform
                   .position
               + new Vector3(0, 1, 0);

        public void SetPosition() {
            if (_switchComponent == null) return;
            var isLargerFloorObject =
                Utilities.GameObjectWidth(_floor.floorGameObjects[0])
                >= Utilities.GameObjectWidth(_floor.floorGameObjects[1]);
            _switchComponent.transform.position = isLargerFloorObject
                ? SwitchPosition(_floor.floorGameObjects[0])
                : SwitchPosition(_floor.floorGameObjects[1]);
        }

        public Switch Generate() {
            _switchComponent = Initialize<Switch>(SwitchParentName);
            return _switchComponent;
        }

        public void SetupPrefab(GameObject prefab) {
            SetupPrefab(_switchComponent.gameObject, prefab);
        }

        public void SetColor(Color color) {
            var colorId = Shader.PropertyToID(
                "_Color"
            );
            _switchComponent
                .gameObject
                .GetComponentInChildren<Renderer>()
                .material
                .SetColor(
                    colorId,
                    color
                );
        }
    }
}
