using UnityEngine;

namespace BlackPad.DropCube.Level.Room {
  public class RoomFactory {
    
    readonly LevelObjectBuilder<RoomGenerator, Room> _roomBuilder;
    readonly RoomGenerator _roomGenerator;
    
    public RoomFactory()
    {
      _roomBuilder = new LevelObjectBuilder<RoomGenerator, Room>();
      _roomGenerator = new RoomGenerator();
    }
    
    public RoomFactory Initialize(Component parent, float roomHeight, int roomNumber) {
      
      _roomGenerator.InitializeGenerator(
        parent,
        roomHeight,
        roomNumber
      );
      
      _roomBuilder.Initialize(
        _roomGenerator,
        null
      );

      return this;
    }

    public Room Build() =>
      _roomBuilder
        .SetPosition()
        .GetProduct(); 
  }
}

