
using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class Door : MonoBehaviour {

        BoxCollider _doorCollider;
        bool _isClosedDoor;
        [SerializeField] GameObject doorObject;

        GameObject _gameManager;
        
        void Awake() {
            _gameManager = GameObject.FindGameObjectWithTag("GameManager");
            SetupDoorPrefab();
            SetupDoorCollider();
        }

        void SetupDoorPrefab() {
            var doorPrefab = _gameManager.GetComponent<RoomManager>()
                .doorPrefab;
            Debug.Log(gameObject.name);
            doorObject = Instantiate(doorPrefab, transform.position, Quaternion.identity);
            doorObject.transform.parent = gameObject.transform;
        }
        
        void SetupDoorCollider() {
            _doorCollider = gameObject.AddComponent<BoxCollider>();
    
            if(!_isClosedDoor) {
                _doorCollider.isTrigger = true;
            }
            
        }

    }
}
