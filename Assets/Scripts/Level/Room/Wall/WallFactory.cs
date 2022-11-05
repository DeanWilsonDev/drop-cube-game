using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Wall {
  public class WallsFactory {

    readonly Component parent;
    readonly float roomHeight;
    readonly float roomWidth;
    readonly Color color;
    readonly LevelObjectBuilder<WallGenerator, Wall> wallBuilder;
    
    public WallsFactory(Component parent, float roomHeight, float roomWidth, Color color) {
      this.parent = parent;
      this.roomHeight = roomHeight;
      this.roomWidth = roomWidth;
      this.color = color;
      
      var wallGenerator = new WallGenerator(
        parent,
        roomHeight,
        roomWidth
      );

      wallBuilder = new LevelObjectBuilder<WallGenerator, Wall>(
        wallGenerator,
        null
      );
    }

    public Wall Build() {
      return wallBuilder
        .SetPosition()
        .SetColor(color)
        .GetProduct();
    }
  }
}