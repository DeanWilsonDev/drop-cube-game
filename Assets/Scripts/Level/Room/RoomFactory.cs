using UnityEngine;

namespace BlackPad.DropCube.Level {
  public class RoomFactory {

    readonly Component parent;
    readonly float roomHeight;
    readonly int roomNumber;
    
    public RoomFactory(Component parent, float roomHeight, int roomNumber) {
      this.parent = parent;
      this.roomHeight = roomHeight;
      this.roomNumber = roomNumber;
    }

    public Room Build() {
      var roomGenerator = new RoomGenerator(
        parent,
        roomHeight,
        roomNumber
      );

      return new LevelObjectBuilder<RoomGenerator, Room>(
          roomGenerator,
          null
        )
        .SetPosition()
        .GetProduct();
    }
  }
}

