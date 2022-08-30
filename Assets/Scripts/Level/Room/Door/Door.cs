
using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class Door : MonoBehaviour {

        BoxCollider _doorCollider;
        [SerializeField] bool isClosedDoor;
        GameObject _doorObject;
        const string DoorPrefabPath = "Prefabs/Door";

        void Awake() {
            isClosedDoor = Random.Range(0, 100) >= 50;
            _doorObject = Resources.Load<GameObject>(DoorPrefabPath);
        }

        void Start() {
            SetupDoorPrefab();
            SetupDoorCollider();
        }

        void SetupDoorPrefab() {
            if (!isClosedDoor) return;
            _doorObject = Instantiate(_doorObject, transform.position, Quaternion.identity);
            _doorObject.transform.parent = gameObject.transform;
        }
        
        void SetupDoorCollider() {
            _doorCollider = gameObject.AddComponent<BoxCollider>();
    
            if(!isClosedDoor) {
                _doorCollider.isTrigger = true;
            }
            
        }

    }
}
