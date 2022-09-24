using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackPad.DropCube.Core {
  public interface IBuilder<out TGenerator, out TComponent>
    where TGenerator : IGenerator<TComponent>
    where TComponent : Component {

    IBuilder<TGenerator, TComponent> SetupPrefab();
    IBuilder<TGenerator, TComponent> SetPosition();
    IBuilder<TGenerator, TComponent> BuildPartC();

    TComponent GetProduct();

  }
}