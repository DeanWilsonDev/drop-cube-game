using BlackPad.Core;
using UnityEngine;

namespace BlackPad.DropCube.Player
{
    public class PlayerManager : Singleton<PlayerManager> {

        public GameObject player;
        [SerializeField] float fallSpeed;
        
        void Initialize() {
            player = GameObject.CreatePrimitive(PrimitiveType.Cube);
            player.name = "Cube";
            var playerInput = player.AddComponent<PlayerInput>();
            var playerComponent = player.AddComponent<Player>();
            playerComponent.Initialize(fallSpeed);
            playerInput.Initialize();
            player.transform.position = Vector3.zero;
        }

        public void Reset() => player.transform.position = Vector3.zero;

        void Awake() => Initialize();
    }
}
