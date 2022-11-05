using UnityEngine;
using BlackPad.DropCube.Level.Room.Floor;

namespace BlackPad.DropCube.Level.Room.Switch {

  public class SwitchFactory {

    readonly Component parent;
    readonly Floor.Floor floor;
    readonly GameObject prefab;
    readonly Color color;
    readonly LevelObjectBuilder<SwitchGenerator, Switch> switchBuilder;

    public SwitchFactory(Component parent, Floor.Floor floor, GameObject prefab, Color color)
    {
      this.parent = parent;
      this.floor = floor;
      this.prefab = prefab;
      this.color = color;
      
      var switchGenerator = new SwitchGenerator(
        parent,
        floor
      );
      switchBuilder = new LevelObjectBuilder<SwitchGenerator, Switch>(switchGenerator, prefab);
    }

    public Switch Build() {
      return switchBuilder
        .SetupPrefab()
        .SetPosition()
        .SetColor(color)
        .GetProduct();
    }
  }
}