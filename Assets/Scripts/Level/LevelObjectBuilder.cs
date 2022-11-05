using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level {
  public class LevelObjectBuilder<TGenerator, TComponent> : IBuilder<TGenerator, TComponent> 
    where TGenerator: IGenerator<TComponent> 
    where TComponent: Component
  {

    TGenerator generator;
    TComponent product;
    readonly GameObject prefab;

    public LevelObjectBuilder(TGenerator generator, GameObject prefab) {
      this.generator = generator;
      this.prefab = prefab;
      Reset();
    }

    void Reset() {
      product = this.generator.Initialize();
    }

    public IBuilder<TGenerator, TComponent> SetupPrefab() {
      this.generator.SetupPrefab(prefab);
      return this;
    }

    public IBuilder<TGenerator, TComponent> SetPosition() {
      this.generator.SetPosition();
      return this;
    }

    public IBuilder<TGenerator, TComponent> SetColor(Color color) {
      this.generator.SetColor(color);
      return this;
    }

    public TComponent GetProduct() {
      return this.product;
    }

  }
}