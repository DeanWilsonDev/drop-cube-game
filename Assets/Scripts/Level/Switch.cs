using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class Switch : MonoBehaviour
    {

        [SerializeField]
        Door doorComponent;
        
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
                doorComponent.OpenDoor();
            }
        }
    }
}