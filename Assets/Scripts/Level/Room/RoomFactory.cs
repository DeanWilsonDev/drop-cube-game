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
      Vector3 roomScale,
      int roomNumber
      )
    {

      var position = parent.transform.position;
      _position = new Vector3(
        position.x,
        position.y - roomScale.y * roomNumber,
        position.z
      );
      

      return _roomBuilder.Initialize(
          RoomName,
          parent,
          _position,
          roomScale,
          null,
          null
        )
        .GenerateEmptyObject()
        .AddComponent()
        .SetPosition()
        .SetScale()
        .GetProduct();
    }

  }
}

