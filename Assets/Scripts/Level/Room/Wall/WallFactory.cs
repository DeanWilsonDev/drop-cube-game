using UnityEngine;

namespace BlackPad.DropCube.Level {
  public class WallsFactory {

    readonly Component parent;
    readonly float roomHeight;
    readonly float roomWidth;

    public WallsFactory(Component parent, float roomHeight, float roomWidth) {
      this.parent = parent;
      this.roomHeight = roomHeight;
      this.roomWidth = roomWidth;
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
        .GetProduct();
    }

  }
}