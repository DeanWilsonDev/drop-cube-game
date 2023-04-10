using System;
using BlackPad.DropCube.Game;
using TMPro;
using UnityEngine;

namespace BlackPad.DropCube.Menus
{
    public class DisplayScore : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _scoreText;
        PointsManager _pointsManager;
        const string GameManagerTag = "GameManager";
        void Awake()
        {
            _pointsManager = GameObject
                .FindGameObjectWithTag(GameManagerTag)
                .GetComponent<PointsManager>();
        }

        void OnEnable()
        {
            PointsManager.OnSetScore += SetScore;
            _pointsManager.SetScoreText();
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
