using BlackPad.Core.Utilities;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room
{
    public class RoomGenerator
        : Generator<Room>
    {
        
        Room _roomComponent;
        float _roomHeight;
        int _roomNumber;


        public RoomGenerator()
        {
            ObjectName = RoomName;
        }

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

        public override void SetPosition()
        {
            
        }
        
    }
}