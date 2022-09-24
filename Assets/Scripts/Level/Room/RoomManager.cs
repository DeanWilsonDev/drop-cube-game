using BlackPad.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level {

    public class RoomManager : Singleton<RoomManager> {

        int _roomNumber = 1;
        [SerializeField] int startingRoomAmount;

        [SerializeField] float roomHeight = 10f;

        [SerializeField] float roomWidth = 50f;

        [SerializeField] float doorSize = 5f;
        

        public float DoorSize => doorSize;

        // Start is called before the first frame update
        void Start() {
            var parent = transform;
            var parentPosition = parent.position;
            for (var i = 0; i < startingRoomAmount; i++) {
                var roomObject = new GameObject {
                    transform = {
                        parent = parent
                    },
                    name = "Room " + _roomNumber
                };
                roomObject.AddComponent<Room>();
                var roomComponent = roomObject.GetComponent<Room>();
                roomComponent.RoomHeight = roomHeight;
                roomComponent.RoomWidth = roomWidth;
                roomObject.transform.position = new Vector3(
                    parentPosition.x,
                    parentPosition.y - (roomComponent.RoomHeight * _roomNumber),
                    parentPosition.z
                );
                _roomNumber++;
            }
        }

    }
}