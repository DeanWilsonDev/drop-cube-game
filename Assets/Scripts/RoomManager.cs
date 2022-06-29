using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    int _roomNumber = 1;
    [SerializeField]
    int startingRoomAmount;
    
    // Start is called before the first frame update
    void Start() {
        var parent = transform;
        var parentPosition = parent.position;
        
        for(var i = 0; i < startingRoomAmount; i++) {
            var roomObject = new GameObject {
                transform = {
                    parent = parent
                },
                name = "Room " + _roomNumber
            };
            roomObject.AddComponent<Room>();
            var roomComponent = roomObject.GetComponent<Room>();

            roomObject.transform.position = new Vector3(
                parentPosition.x,
                parentPosition.y - (roomComponent.RoomHeight * _roomNumber),
                parentPosition.z
            );
            _roomNumber++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
