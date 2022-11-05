using UnityEngine;
using BlackPad.DropCube.Level.Room.Floor;

namespace BlackPad.DropCube.Level.Room.Switch {

  public class SwitchFactory {

    readonly Component parent;
    readonly Floor.Floor floor;
    readonly GameObject prefab;
    readonly Color color;

    public SwitchFactory(Component parent, Floor.Floor floor, GameObject prefab, Color color) {
      this.parent = parent;
      this.floor = floor;
      this.prefab = prefab;
      this.color = color;
    }

    public Level.Switch Build() {
      var switchGenerator = new SwitchGenerator(
        parent,
        floor
      );
      return new LevelObjectBuilder<SwitchGenerator, Level.Switch>(
          switchGenerator,
          prefab
        )
        .SetupPrefab()
        .SetPosition()
        .SetColor(color)
        .GetProduct();
    }

  }
}