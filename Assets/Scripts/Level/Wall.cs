using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class Wall : MonoBehaviour
    {
        void Start()
        {
            gameObject.layer = 6;
        }
    }
}