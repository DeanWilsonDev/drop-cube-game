using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class Door : MonoBehaviour {

        BoxCollider _doorCollider;
        GameObject _doorObject;
        Renderer[] _prefabRenderers;
        BoxCollider _boxCollider;
        Animator _animator;
        
        static readonly int Open = Animator.StringToHash("Open");

        void Start()
        {
            _boxCollider = gameObject.AddComponent<BoxCollider>();
            _animator = gameObject.GetComponentInChildren<Animator>();

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
        }
    }
}