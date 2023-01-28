using UnityEngine;

namespace BlackPad.DropCube.Level
{
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
      var transform = parent.transform;

      var room = new GameObject
      {
        gameObject =
        {
          name = "Room " + roomNumber
        },
        transform =
        {
          parent = transform,
          position = new Vector3(
            0,
            transform.position.y - roomScale.y * roomNumber,
            0
          ),
          localScale = roomScale
        }
      };
      
      return room.AddComponent<Room>();
    }
    

  }
}