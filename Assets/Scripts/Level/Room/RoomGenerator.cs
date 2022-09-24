using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using UnityEditor;
using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class RoomGenerator : Generator, IGenerator<Room> {
        
        const string RoomName = "Room";
        Room roomComponent;
        readonly float roomHeight;
        readonly int roomNumber;
        
        public RoomGenerator(Component parent, float roomHeight, int roomNumber) {
            Parent = parent;
            this.roomHeight = roomHeight;
            this.roomNumber = roomNumber;
        }

        public void SetPosition() {
            roomComponent.transform.position = new Vector3(
                Utilities.GameObjectTransformPosition(Parent.gameObject)
                    .x,
                Utilities.GameObjectTransformPosition(Parent.gameObject)
                    .y
                - roomHeight * roomNumber,
                Utilities.GameObjectTransformPosition(Parent.gameObject)
                    .z
            );
        }
        
        public Room Initialize() {
            roomComponent = this.Initialize<Room>(RoomName);
            return roomComponent;
        }

        public void SetupPrefab(GameObject prefab) { 
            // this.SetupPrefab(roomComponent.gameObject, prefab);
        }
    }
}
