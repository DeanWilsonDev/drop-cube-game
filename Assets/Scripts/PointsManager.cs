
using UnityEngine;

namespace BlackPad.DropCube
{
    public class PointsManager: MonoBehaviour
    {

        [SerializeField] int _currentPoints;
        
        public int CurrentPoints
        {
            get => _currentPoints;
            set => _currentPoints = value;
        }

        public void Start()
        {
            CurrentPoints = 0;
        }
    }
}