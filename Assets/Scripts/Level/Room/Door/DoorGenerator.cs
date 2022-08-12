using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public static class DoorGenerator
    {

        public static Door InitializeDoor(Component parent, float doorSize) {
            var parentTransform = parent.transform;
                
            var doorObject = new GameObject() {
                transform = {
                    position = parentTransform.position,
                    localScale = new Vector3(doorSize, 1, 5),
                    parent = parentTransform
                },
                name = "Door"
            };
            
            var door = doorObject.AddComponent<Door>();
            
            return door;
        }
    }
}
