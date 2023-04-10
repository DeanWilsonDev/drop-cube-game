using UnityEngine;

namespace BlackPad.DropCube.Player
{
    public class PlayerManager: MonoBehaviour {

         public GameObject _playerPrefab;
         public GameObject _player;
        [SerializeField] float _fallSpeed;
        
        public void Initialize() {
            _player = Instantiate(_playerPrefab);
            var playerInput = _player.GetComponent<PlayerInput>();
            var playerComponent = _player.GetComponent<Player>();
            playerComponent.Initialize(_fallSpeed);
            playerInput.Initialize();
            _player.transform.position = Vector3.zero;
        }

        public void Reset() => _player.transform.position = Vector3.zero;

        public void Kill()
        {
            Destroy(_player);
        }
    }
}