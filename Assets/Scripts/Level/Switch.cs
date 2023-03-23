using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class Switch : MonoBehaviour
    {

        [SerializeField]
        Door doorComponent;

        [SerializeField] Light _pointLight;
        [SerializeField] Material SwitchGreen;
        [SerializeField] Material SwitchRed;

        void Start()
        {
            var boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider.isTrigger = true;
            doorComponent = GetComponentInParent<Room>().DoorComponent;
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            doorComponent.OpenDoor();
            _pointLight.color = SwitchGreen.color;
        }
    }
}