using BlackPad.Core.Utilities;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room {
  public class RoomFactory {
    
    readonly LevelObjectBuilder<Room> _roomBuilder;
    const string RoomName = "Room";
    Vector3 _position;
    
    public RoomFactory()
    {
      _roomBuilder = new LevelObjectBuilder<Room>();
    }
    
    public Room Build(
      Component parent,
      float roomHeight,
      int roomNumber
      ) {

      _position = new Vector3(
        Utilities.GameObjectTransformPosition(parent.gameObject)
          .x,
        Utilities.GameObjectTransformPosition(parent.gameObject)
          .y
        - roomHeight * roomNumber,
        Utilities.GameObjectTransformPosition(parent.gameObject)
          .z
      );

      return _roomBuilder.Initialize(
          RoomName,
          parent,
          _position,
          null,
          null,
          null
        )
        .Generate()
        .SetPosition()
        .GetProduct();

    }

  }
}

