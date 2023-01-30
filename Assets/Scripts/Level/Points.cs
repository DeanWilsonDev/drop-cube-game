using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlackPad.DropCube.Level
{
    public class Points : MonoBehaviour
    {
        TextMeshPro _pointsText;

        void Start()
        {
            _pointsText = GetComponent<TextMeshPro>();
        }
        
        public void DisplayCurrentPointsValue(string value)
        {
            _pointsText.text = value;
        }
    }
}