
using UnityEngine;

namespace BlackPad.DropCube.Player
{
    public class Player : MonoBehaviour {

        [SerializeField] float fallSpeed;
        Rigidbody rb;
        
        public void Initialize(float fallSpeed) {
            this.fallSpeed = fallSpeed;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            rb ??= gameObject.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.AddForce(Vector3.down * (fallSpeed * Time.deltaTime), ForceMode.Acceleration);
        }
        
        
        
    }
}