using BlackPad.Core.Utilities;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Door {

  public class DoorFactory {
    
    readonly LevelObjectBuilder<Door> _doorLevelObjectBuilder;
    const string DoorObjectName = "Door";
    
    public DoorFactory()
    {
      _doorLevelObjectBuilder = new LevelObjectBuilder<Door>();
    }
    
    public Door Build(
      Component parent,
      Floor.Floor floor,
      float doorSize,
      GameObject prefab,
      Color color
    ) {
      
      var doorPosition = new Vector3(
        Utilities.GameObjectTransformPosition(floor.floorGameObjects[0])
          .x
        + Utilities.GameObjectWidth(floor.floorGameObjects[0]) / 2
        + doorSize / 2,
        Utilities.GameObjectTransformPosition(floor.floorGameObjects[0])
          .y,
        Utilities.GameObjectTransformPosition(floor.floorGameObjects[0])
          .z
      );
      var doorScale = new Vector3(doorSize, 1, 5);
      
      return _doorLevelObjectBuilder.Initialize(
        DoorObjectName,
        parent,
        doorPosition,
        doorScale,
        prefab,
        color
      )
        .GeneratePrefabObject()
        .AddComponent()
        .SetPosition()
        .SetColor()
        .GetProduct();
    }
    
  }
}