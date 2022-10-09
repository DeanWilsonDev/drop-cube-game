using UnityEngine;

namespace BlackPad.DropCube.Level {
  public class WallsFactory {

    readonly Component parent;
    readonly float roomHeight;
    readonly float roomWidth;
    readonly Color color;

    public WallsFactory(Component parent, float roomHeight, float roomWidth, Color color) {
      this.parent = parent;
      this.roomHeight = roomHeight;
      this.roomWidth = roomWidth;
      this.color = color;
    }

    public Wall Build() {
      var wallGenerator = new WallGenerator(
        parent,
        roomHeight,
        roomWidth
      );
      return new LevelObjectBuilder<WallGenerator, Wall>(
          wallGenerator,
          null
        ).SetPosition()
        .SetColor(color)
        .GetProduct();
    }

  }
}