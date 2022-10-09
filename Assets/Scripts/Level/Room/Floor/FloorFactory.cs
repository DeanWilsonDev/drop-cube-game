using UnityEngine;

namespace BlackPad.DropCube.Level {
  public class FloorFactory {

    readonly Component parent;
    readonly float roomWidth;
    readonly float doorSize;
    readonly Color color;

    public FloorFactory(Component parent, float roomWidth, float doorSize, Color color) {
      this.parent = parent;
      this.roomWidth = roomWidth;
      this.doorSize = doorSize;
      this.color = color;
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
        .SetColor(color)
        .GetProduct();
    }

  }
}