using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Door {

  public class DoorFactory {
    
    Color _color;
    
    readonly LevelObjectBuilder<DoorGenerator, Door> _doorBuilder;
    readonly DoorGenerator _doorGenerator;
    
    public DoorFactory()
    {
      _doorGenerator = new DoorGenerator();
      _doorBuilder = new LevelObjectBuilder<DoorGenerator, Door>();
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

      _doorBuilder.Initialize(
        _doorGenerator,
        prefab
      );
      
      return this;
    }

    public Door Build() => 
      _doorBuilder
        .SetupPrefab()
        .SetPosition()
        .SetColor(_color)
        .GetProduct();
  }
}