using System.Linq;
using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Door {
  public class DoorGenerator : Generator, IGenerator<Door> {

    float _size;
    Floor.Floor _floor;
    const string DoorParentName = "Door";
    Door _doorComponent;

    public DoorGenerator InitializeGenerator(Component parent, float size, Floor.Floor floor) {
      Parent = parent;
      _size = size;
      _floor = floor;
      return this;
    }

    public void SetPosition() {
      if (_doorComponent == null) return;
      _doorComponent.transform.position = new Vector3(
        Utilities.GameObjectTransformPosition(_floor.floorGameObjects[0])
          .x
        + Utilities.GameObjectWidth(_floor.floorGameObjects[0]) / 2
        + _size / 2,
        Utilities.GameObjectTransformPosition(_floor.floorGameObjects[0])
          .y,
        Utilities.GameObjectTransformPosition(_floor.floorGameObjects[0])
          .z
      );
    }

    public Door Initialize() {
      _doorComponent = this.Initialize<Door>(DoorParentName);
      _doorComponent.transform.localScale = new Vector3(_size, 1, 5);
      return _doorComponent;
    }

    public void SetupPrefab(GameObject doorPrefab) {
      SetupPrefab(_doorComponent.gameObject, doorPrefab);
    }

    public void SetColor(Color color) {
      var colorId = Shader.PropertyToID(
        "_Color"
      );

      foreach (var renderer in _doorComponent
                 .gameObject
                 .GetComponentsInChildren<Renderer>()
                 .Select(
                   renderer => {
                     renderer.material.SetColor(
                       colorId,
                       color
                     );
                     return renderer;
                   }
                 )
              )
        ;
    }

  }
}