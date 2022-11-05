using System;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Door
{
    public class Door : MonoBehaviour {

        BoxCollider doorCollider;
        GameObject doorObject;
        Renderer[] prefabRenderers;

        void Start()
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }
}
