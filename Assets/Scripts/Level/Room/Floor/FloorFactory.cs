using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Floor {
  public class FloorFactory {

    readonly Component parent;
    readonly float roomWidth;
    readonly float doorSize;
    readonly Color color;

    readonly LevelObjectBuilder<FloorGenerator, Floor> floorBuilder;
    
    public FloorFactory(Component parent, float roomWidth, float doorSize, Color color) {
      this.parent = parent;
      this.roomWidth = roomWidth;
      this.doorSize = doorSize;
      this.color = color;
      var floorGenerator = new FloorGenerator(
        parent,
        roomWidth,
        doorSize
      );
      floorBuilder = new LevelObjectBuilder<FloorGenerator, Floor>(floorGenerator, null);
    }

    public Floor Build() {
      return floorBuilder
        .SetupPrefab()
        .SetPosition()
        .SetColor(color)
        .GetProduct();
    }
  }
}