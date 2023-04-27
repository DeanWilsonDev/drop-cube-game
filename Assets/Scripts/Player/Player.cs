
using UnityEngine;

namespace BlackPad.DropCube.Player
{
    public class Player : MonoBehaviour {

        [SerializeField] float fallSpeed;
        Rigidbody rb;
        FallHandler _fallHandler;
        Animator _animator;
        static readonly int Falling = Animator.StringToHash("IsFalling");
        
        public void Initialize(float fallSpeed) {
            this.fallSpeed = fallSpeed;
        }
        
        // Start is called before the first frame update
        void Start()
        {
            rb ??= gameObject.GetComponent<Rigidbody>();
            _fallHandler = gameObject.GetComponent<FallHandler>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.AddForce(Vector3.down * (fallSpeed * Time.deltaTime), ForceMode.Acceleration);
            PlayFallingAnim();
        }

        void PlayFallingAnim()
        {
            if (!_fallHandler.IsGrounded)
            {
                _animator.SetTrigger(Falling);
                // _doorAudioSource.Play();
            }
        }
    }
}