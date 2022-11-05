using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Door {

  public class DoorFactory {

    readonly Component parent;
    readonly Floor.Floor floor;
    readonly float doorSize;
    readonly GameObject prefab;
    readonly Color color;
    readonly LevelObjectBuilder<DoorGenerator, Door> doorBuilder;
    
    public DoorFactory(
      Component parent,
      Floor.Floor floor,
      float doorSize,
      GameObject prefab,
      Color color
    ) {
      this.parent = parent;
      this.floor = floor;
      this.doorSize = doorSize;
      this.prefab = prefab;
      this.color = color;
      
      var doorGenerator = new DoorGenerator(
        parent,
        doorSize,
        floor
      );

      doorBuilder = new LevelObjectBuilder<DoorGenerator, Door>(
        doorGenerator,
        prefab
      );
    }

    public Door Build() {
      return doorBuilder
        .SetupPrefab()
        .SetPosition()
        .SetColor(color)
        .GetProduct();
    }
  }
}