using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Wall
{
    public class Wall : MonoBehaviour
    {
        public List<GameObject> wallGameObjects = new ();


        void Start()
        {
            gameObject.layer = 6;
        }
    }

}
