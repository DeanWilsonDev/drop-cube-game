using BlackPad.DropCube.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace BlackPad.DropCube.Player
{
    public class PlayerManager : Singleton<PlayerManager> {

        [FormerlySerializedAs("player")] public GameObject _player;
        [FormerlySerializedAs("fallSpeed")] [SerializeField] float _fallSpeed;
        
        void Initialize() {
            _player = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _player.name = "Cube";
            var playerInput = _player.AddComponent<PlayerInput>();
            var playerComponent = _player.AddComponent<Player>();
            _player.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Color");
            playerComponent.Initialize(_fallSpeed);
            playerInput.Initialize();
            _player.transform.position = Vector3.zero;
        }

        public void Reset() => _player.transform.position = Vector3.zero;

        void Awake() => Initialize();
    }
}