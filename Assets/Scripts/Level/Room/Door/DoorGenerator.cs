using System;
using System.Linq;
using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level {
  public class DoorGenerator : Generator, IGenerator<Door> {

    readonly float size;
    readonly Floor floor;
    const string DoorParentName = "Door";
    Door doorComponent;

    public DoorGenerator(Component parent, float size, Floor floor) {
      Parent = parent;
      this.size = size;
      this.floor = floor;
    }

    public void SetPosition() {
      if (doorComponent == null) return;
      doorComponent.transform.position = new Vector3(
        Utilities.GameObjectTransformPosition(floor.floorGameObjects[0])
          .x
        + Utilities.GameObjectWidth(floor.floorGameObjects[0]) / 2
        + size / 2,
        Utilities.GameObjectTransformPosition(floor.floorGameObjects[0])
          .y,
        Utilities.GameObjectTransformPosition(floor.floorGameObjects[0])
          .z
      );
    }

    public Door Initialize() {
      doorComponent = this.Initialize<Door>(DoorParentName);
      doorComponent.transform.localScale = new Vector3(size, 1, 5);
      return doorComponent;
    }

    public void SetupPrefab(GameObject doorPrefab) {
      SetupPrefab(doorComponent.gameObject, doorPrefab);
    }

    public void SetColor(Color color) {
      var colorId = Shader.PropertyToID(
        "_Color"
      );

      foreach (var renderer in doorComponent
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