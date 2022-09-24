using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class FloorFactory {

        readonly Component parent;
        readonly float roomWidth;
        readonly float doorSize;
        
        public FloorFactory(Component parent, float roomWidth, float doorSize) {
            this.parent = parent;
            this.roomWidth = roomWidth;
            this.doorSize = doorSize;
        }
        
        public Floor Build() {
            var floorGenerator = new FloorGenerator(
                parent,
                roomWidth,
                doorSize
            );

            return new LevelObjectBuilder<FloorGenerator, Floor>(
                    floorGenerator,
                    null
                )
                .SetupPrefab()
                .SetPosition()
                .GetProduct();
        }

    }
}
