using System;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Door
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
        }

        public void OpenDoor()
        {
            if (_animator == null) return;
            _boxCollider.enabled = false;
            _animator.SetTrigger(Open);
        }
    }
}
