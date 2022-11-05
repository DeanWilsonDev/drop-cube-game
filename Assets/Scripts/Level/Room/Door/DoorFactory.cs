using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Door {

  public class DoorFactory {

    readonly Component parent;
    readonly Floor.Floor floor;
    readonly float doorSize;
    readonly GameObject prefab;
    readonly Color color;

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
    }

    public Door Build() {
      var doorGenerator = new DoorGenerator(
        parent,
        doorSize,
        floor
      );
      return new LevelObjectBuilder<DoorGenerator, Door>(
          doorGenerator,
          prefab
        )
        .SetupPrefab()
        .SetPosition()
        .SetColor(color)
        .GetProduct();
    }

  }
}