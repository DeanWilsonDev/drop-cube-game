
using System.Collections;
using UnityEngine;

namespace BlackPad.DropCube.Game
{
    public class PointsManager: MonoBehaviour
    {
        public delegate void SetScoreEvent(int currentPoints);
        public static SetScoreEvent OnSetScore;
        [SerializeField] int _currentPoints;
        [SerializeField] AudioSource _audioSource;
        public int CurrentPoints
        {
            get => _currentPoints;
            set => _currentPoints = value;
        }

        public void Initialize()
        {
            CurrentPoints = 0;
        }

        public void SetScoreText()
        {
            OnSetScore?.Invoke(_currentPoints);
        }

        public void Reset()
        {
            _currentPoints = 0;
            SetScoreText();
        }

        public void PlayPointsUpSound()
        {
            _audioSource.Play();
        }
    }
}