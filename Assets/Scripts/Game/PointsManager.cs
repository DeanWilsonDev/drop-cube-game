
using UnityEngine;

namespace BlackPad.DropCube.Game
{
    public class PointsManager: MonoBehaviour
    {

        [SerializeField] int _currentPoints;
        
        public int CurrentPoints
        {
            get => _currentPoints;
            set => _currentPoints = value;
        }

        public void Initialize()
        {
            CurrentPoints = 0;
        }
    }
}