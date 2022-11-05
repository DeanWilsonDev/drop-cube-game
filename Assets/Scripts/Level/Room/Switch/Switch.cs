using System;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Switch
{
    public class Switch : MonoBehaviour
    {

        [SerializeField]
        Door.Door doorComponent;
        
        void Start()
        {
            var boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            doorComponent = GetComponentInParent<Room>().DoorComponent;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Triggered");
                doorComponent.OpenDoor();
            }
        }
    }
}
