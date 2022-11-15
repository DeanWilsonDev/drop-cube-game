using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room
{
    public class RoomGenerator
        : Generator,
            IGenerator<Room>
    {
        const string RoomName = "Room";
        Room _roomComponent;
        float _roomHeight;
        int _roomNumber;

        public RoomGenerator InitializeGenerator(
            Component parent,
            float roomHeight,
            int roomNumber
        )
        {
            Parent = parent;
            _roomHeight = roomHeight;
            _roomNumber = roomNumber;
            return this;
        }

        public void SetPosition()
        {
            _roomComponent.transform.position = new Vector3(
                Utilities.GameObjectTransformPosition(Parent.gameObject)
                    .x,
                Utilities.GameObjectTransformPosition(Parent.gameObject)
                    .y
                - _roomHeight * _roomNumber,
                Utilities.GameObjectTransformPosition(Parent.gameObject)
                    .z
            );
        }

        public Room Initialize()
        {
            _roomComponent = this.Initialize<Room>(RoomName);
            return _roomComponent;
        }

        public void SetupPrefab(GameObject prefab)
        {
            // this.SetupPrefab(roomComponent.gameObject, prefab);
        }

        public void SetColor(Color color)
        {
        }
    }
}