using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Door {

  public class DoorFactory {
    
    Color _color;
    
    readonly LevelObjectBuilder<> _doorLevelObjectBuilder;
    readonly DoorGenerator _doorGenerator;
    
    public DoorFactory()
    {
      _doorGenerator = new DoorGenerator();
      _doorLevelObjectBuilder = new LevelObjectBuilder<>();
    }
    
    public DoorFactory Initialize(
      Component parent,
      Floor.Floor floor,
      float doorSize,
      GameObject prefab,
      Color color
    ) {
      _color = color;
      
      _doorGenerator.InitializeGenerator(
        parent,
        doorSize,
        floor
      );

      _doorLevelObjectBuilder.Initialize(
        _doorGenerator,
        prefab
      );
      
      return this;
    }

    public Door Build() => 
      _doorLevelObjectBuilder
        .SetupPrefab()
        .SetPosition()
        .SetColor(_color)
        .GetProduct();
  }
}