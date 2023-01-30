using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class Points : MonoBehaviour
    {
        
        int _roomScoreValue;

        void Start()
        {
            Room.OnRoomEnter += DisplayCurrentPointsValue;
        }

        void Initialize(int roomScoreValue)
        {
            _roomScoreValue = roomScoreValue;
        }

        void DisplayCurrentPointsValue()
        {
            // _roomScoreValue = PointsManager.Instance.CurrentPoints;
        }
        
    }
}