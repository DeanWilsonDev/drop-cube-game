using UnityEngine;

namespace BlackPad.DropCube.Core
{
  public interface ILevelObjectBuilder<out TComponent>
    where TComponent : Component {
    public ILevelObjectBuilder<TComponent> GeneratePrimitiveObject();
    ILevelObjectBuilder<TComponent> GeneratePrefabObject();
    ILevelObjectBuilder<TComponent> GenerateEmptyObject();

    ILevelObjectBuilder<TComponent> AddComponent();

    ILevelObjectBuilder<TComponent> SetPosition();
    ILevelObjectBuilder<TComponent> SetScale();
    ILevelObjectBuilder<TComponent> SetColor();
    TComponent GetProduct();
    GameObject GetGeneratedObject();
  }
}