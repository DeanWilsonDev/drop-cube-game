using UnityEngine;

namespace BlackPad.DropCube.Core {
  public interface ILevelObjectBuilder<out TComponent>
    where TComponent : Component {
    ILevelObjectBuilder<TComponent> Generate();
    ILevelObjectBuilder<TComponent> SetupPrefab();
    ILevelObjectBuilder<TComponent> SetPosition();
    ILevelObjectBuilder<TComponent> SetScale();
    ILevelObjectBuilder<TComponent> SetColor();
    TComponent GetProduct();
  }
}