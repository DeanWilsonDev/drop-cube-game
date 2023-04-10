using System.Collections;
using TMPro;
using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class Points : MonoBehaviour
    {
        TextMeshPro _pointsText;
        int _pointsValue;
        
        void Start()
        {
            _pointsText = GetComponent<TextMeshPro>();
        }
        
        public void DisplayCurrentPointsValue(int value)
        {
            _pointsValue = value;
            StartCoroutine(ScoreUpdater());
        }
        
        IEnumerator ScoreUpdater()
        {
            var currentPoints = int.Parse(_pointsText.text);
            var difference = _pointsValue - currentPoints;
            while (true)
            {
                if (!_pointsText) break;
                yield return new WaitForSeconds(0.1f);

                const int constantTerm = 1;
                var proportionalTerm = difference / 5;
                var moveStep = Mathf.Abs(proportionalTerm) + constantTerm;
                currentPoints = (int) Mathf.MoveTowards(currentPoints, _pointsValue,
                    moveStep);
                _pointsText.text = currentPoints.ToString();
            }
        }
    } 
}