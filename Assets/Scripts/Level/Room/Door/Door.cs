using System;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Door
{
    public class Door : MonoBehaviour {

        BoxCollider doorCollider;
        GameObject doorObject;
        Renderer[] prefabRenderers;
        BoxCollider boxCollider;
        Animation anim;
        
        void Start()
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }

        public void OpenDoor()
        {
            boxCollider.enabled = false;
        }
    }
}
