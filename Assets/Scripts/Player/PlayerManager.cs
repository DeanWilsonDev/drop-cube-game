using UnityEngine;

namespace BlackPad.DropCube.Player
{
    public class PlayerManager : MonoBehaviour {

        public GameObject player;

        void Initialize() {
            player = GameObject.CreatePrimitive(PrimitiveType.Cube);
            player.name = "Cube";
            var rb = player.AddComponent<Rigidbody>();
            rb.useGravity = true;
            var playerInput = player.AddComponent<PlayerInput>();
            playerInput.Initialize();
            player.transform.position = Vector3.zero;
        }

        public void Reset() => player.transform.position = Vector3.zero;

        // Start is called before the first frame update
        void Start() => Initialize();
    }
}
