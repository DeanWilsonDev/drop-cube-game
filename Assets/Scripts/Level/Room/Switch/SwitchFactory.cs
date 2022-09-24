using UnityEngine;

namespace BlackPad.DropCube.Level {

  public class SwitchFactory {

    readonly Component parent;
    readonly Floor floor;
    readonly GameObject prefab;

    public SwitchFactory(Component parent, Floor floor, GameObject prefab) {
      this.parent = parent;
      this.floor = floor;
      this.prefab = prefab;
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
        .GetProduct();
    }

  }
}