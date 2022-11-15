using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level {
  public class LevelObjectBuilder
    <TGenerator, TComponent> : IBuilder<TGenerator, TComponent>
    where TGenerator : IGenerator<TComponent>
    where TComponent : Component
  {

    TGenerator _generator;
    TComponent _product;
    GameObject _prefab;

    public void Initialize(TGenerator generator, GameObject prefab) {
      _generator = generator;
      _prefab = prefab;
      Reset();
    }

    void Reset() {
      _product = _generator.Initialize();
    }

    public IBuilder<TGenerator, TComponent> SetupPrefab() {
      this._generator.SetupPrefab(_prefab);
      return this;
    }

    public IBuilder<TGenerator, TComponent> SetPosition() {
      _generator.SetPosition();
      return this;
    }

    public IBuilder<TGenerator, TComponent> SetColor(Color color) {
      _generator.SetColor(color);
      return this;
    }

    public TComponent GetProduct() {
      return _product;
    }

  }
}