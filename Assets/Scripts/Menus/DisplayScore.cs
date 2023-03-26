using BlackPad.DropCube.Game;
using TMPro;
using UnityEngine;

namespace BlackPad.DropCube.Menus
{
    public class DisplayScore : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _scoreText;

        void OnEnable()
        {
            PointsManager.OnSetScore += SetScore;
        }

        void OnDisable()
        {
            PointsManager.OnSetScore -= SetScore;
        }

        public void SetScore(int currentPoints)
        {
            _scoreText.text = currentPoints.ToString();
        }
    }
}
