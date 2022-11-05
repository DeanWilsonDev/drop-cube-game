using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Switch
{
    public class SwitchGenerator: Generator, IGenerator<Switch> {

        const string SwitchParentName = "Switch";
        readonly Floor.Floor floor;
        Switch switchComponent;
        
        public SwitchGenerator(Component parent, Floor.Floor floor) {
            Parent = parent;
            this.floor = floor;
        }
        
        static Vector3 SwitchPosition(GameObject floorObject)
            => floorObject
                   .transform
                   .position
               + new Vector3(0, 1, 0);

        public void SetPosition() {
            if (switchComponent == null) return;
            var isLargerFloorObject =
                Utilities.GameObjectWidth(floor.floorGameObjects[0])
                >= Utilities.GameObjectWidth(floor.floorGameObjects[1]);
            switchComponent.transform.position = isLargerFloorObject
                ? SwitchPosition(floor.floorGameObjects[0])
                : SwitchPosition(floor.floorGameObjects[1]);
        }

        public Switch Initialize() {
            switchComponent = Initialize<Switch>(SwitchParentName);
            return switchComponent;
        }

        public void SetupPrefab(GameObject prefab) {
            SetupPrefab(switchComponent.gameObject, prefab);
        }

        public void SetColor(Color color) {
            var colorId = Shader.PropertyToID(
                "_Color"
            );
            switchComponent
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
