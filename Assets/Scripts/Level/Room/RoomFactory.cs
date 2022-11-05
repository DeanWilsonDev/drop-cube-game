using UnityEngine;

namespace BlackPad.DropCube.Level.Room {
  public class RoomFactory {

    readonly Component parent;
    readonly float roomHeight;
    readonly int roomNumber;
    readonly LevelObjectBuilder<RoomGenerator, Room> roomBuilder;
    
    public RoomFactory(Component parent, float roomHeight, int roomNumber) {
      this.parent = parent;
      this.roomHeight = roomHeight;
      this.roomNumber = roomNumber;

      var roomGenerator = new RoomGenerator(
        parent,
        roomHeight,
        roomNumber
      );
      
      roomBuilder = new LevelObjectBuilder<RoomGenerator, Room>(
        roomGenerator,
        null
      );
    }

    public Room Build() {
      return roomBuilder
        .SetPosition()
        .GetProduct();
    }
  }
}

