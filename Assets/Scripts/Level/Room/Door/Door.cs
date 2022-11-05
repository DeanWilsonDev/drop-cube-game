using System;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Door
{
    public class Door : MonoBehaviour {

        BoxCollider doorCollider;
        GameObject doorObject;
        Renderer[] prefabRenderers;
        BoxCollider boxCollider;
        Animator animator;
        static readonly int Open = Animator.StringToHash("Open");

        void Start()
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
            animator = gameObject.GetComponentInChildren<Animator>();
        }

        public void OpenDoor()
        {
            if (animator == null) return;
            boxCollider.enabled = false;
            animator.SetTrigger(Open);
        }
    }
}
