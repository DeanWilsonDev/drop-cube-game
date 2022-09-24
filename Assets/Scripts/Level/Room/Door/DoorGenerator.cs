using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class DoorGenerator {

        readonly Component parent;
        readonly float size;

        public DoorGenerator(Component floor, float doorSize) {
            parent = floor;
            size = doorSize;
        }
        
        public Door InitializeDoor() {
            var parentTransform = parent.transform;
                
            var doorParent = new GameObject() {
                transform = {
                    position = parentTransform.position,
                    localScale = new Vector3(size, 1, 5),
                    parent = parentTransform
                },
                name = "Door"
            };
            var door = doorParent.AddComponent<Door>();
            return door;
        }
    }
}
