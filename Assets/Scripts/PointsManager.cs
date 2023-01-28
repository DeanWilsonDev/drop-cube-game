
using BlackPad.DropCube.Core;

namespace BlackPad.DropCube
{
    public class PointsManager: Singleton<PointsManager>
    {
        public int CurrentPoints { get; set; }

        public void Start()
        {
            CurrentPoints = 0;
        }
    }
}