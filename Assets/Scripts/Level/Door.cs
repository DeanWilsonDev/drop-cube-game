using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class Door : MonoBehaviour {

        BoxCollider _doorCollider;
        GameObject _doorObject;
        Renderer[] _prefabRenderers;
        BoxCollider _boxCollider;
        Animator _animator;
        [SerializeField] AudioSource _doorAudioSource;

        static readonly int Open = Animator.StringToHash("Open");

        void Start()
        {
            _boxCollider = gameObject.AddComponent<BoxCollider>();
            _animator = GetComponentInChildren<Animator>();
            _doorAudioSource = GetComponent<AudioSource>();
            _boxCollider.size = new Vector3(
                5.5f,
                1, 
                5
            );
        }

        public void OpenDoor()
        {
            if (_animator == null) return;
            _boxCollider.enabled = false;
            _animator.SetTrigger(Open);
            _doorAudioSource.Play();
        }
    }
}