using UnityEngine;

namespace BlackPad.DropCube.Level {

  public class SwitchFactory {

    readonly Component parent;
    readonly Floor floor;
    readonly GameObject prefab;
    readonly Color color;

    public SwitchFactory(Component parent, Floor floor, GameObject prefab, Color color) {
      this.parent = parent;
      this.floor = floor;
      this.prefab = prefab;
      this.color = color;
    }

    public Switch Build() {
      var switchGenerator = new SwitchGenerator(
        parent,
        floor
      );
      return new LevelObjectBuilder<SwitchGenerator, Switch>(
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