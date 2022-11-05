using System.Collections.Generic;
using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level.Room.Wall {

  public class WallGenerator : Generator, IGenerator<Wall> {

    const string WallsParentObjectName = "Walls";
    const string LeftWallName = "Left Wall";
    const string RightWallName = "Right Wall";
    const string BackWallName = "Back Wall";

    GameObject walls;
    readonly float roomHeight;
    readonly Component parent;
    readonly float roomWidth;
    BackWall backWall;
    Wall rightWall;
    Wall leftWall;

    public WallGenerator(Component parent, float roomHeight, float roomWidth) {
      this.parent = parent;
      this.roomHeight = roomHeight;
      this.roomWidth = roomWidth;
    }

    BackWall GenerateBackWall() {
      var backWallObject = GameObject.CreatePrimitive(
        PrimitiveType.Cube
      );
      backWallObject.name = BackWallName;
      backWallObject.transform.localScale = new Vector3(
        roomWidth,
        roomHeight,
        1
      );
      backWallObject.transform.parent = parent.transform;
      return backWallObject.AddComponent<BackWall>();
    }

    Wall GenerateWall(string wallObjectName) {
      var wall = GameObject.CreatePrimitive(
        PrimitiveType.Cube
      );
      wall.transform.localScale = new Vector3(
        1,
        roomHeight,
        5
      );
      var parentTransform = parent.transform;
      var parentPosition = parentTransform.position;
      wall.transform.position = new Vector3(
        parentPosition.x,
        parentPosition.y + (wall.transform.localScale.y / 2),
        parentPosition.z
      );
      wall.gameObject.name = wallObjectName;
      return wall.AddComponent<Wall>();
    }

    GameObject GenerateWalls() {
      this.leftWall = GenerateWall(
        LeftWallName
      );

      this.rightWall = GenerateWall(
        RightWallName
      );

      backWall = GenerateBackWall();
      walls = new GameObject {
        name = WallsParentObjectName
      };

      leftWall.transform.parent = walls.transform;
      rightWall.transform.parent = walls.transform;
      backWall.transform.parent = walls.transform;
      walls.transform.parent = parent.transform;

      var wallComponent = walls.AddComponent<Wall>();

      wallComponent.wallGameObjects = new List<GameObject> {
        leftWall.gameObject,
        rightWall.gameObject,
        backWall.gameObject
      };

      return walls;
    }

    public Wall Initialize() {
      return GenerateWalls()
        .GetComponent<Wall>();
    }

    public void SetupPrefab(GameObject prefab) { }

    public void SetPosition() {
      rightWall.transform.position = new Vector3(
        Utilities.GameObjectTransformPosition(
            parent.gameObject
          )
          .x
        + roomWidth,
        rightWall.transform.position.y,
        Utilities.GameObjectTransformPosition(
            parent.gameObject
          )
          .z
      );

      backWall.transform.position = new Vector3(
        Utilities.GameObjectTransformPosition(
            parent.gameObject
          )
          .x
        + roomWidth / 2,
        Utilities.GameObjectTransformPosition(
            parent.gameObject
          )
          .y
        + roomHeight / 2,
        Utilities.GameObjectTransformPosition(
            parent.gameObject
          )
          .z
        + 2.5f
      );
    }

    public void SetColor(Color color) {
      var colorId = Shader.PropertyToID(
        "_Color"
      );
      backWall
        .gameObject
        .GetComponent<Renderer>()
        .material
        .SetColor(
          colorId,
          color
        );

      leftWall
        .gameObject
        .GetComponent<Renderer>()
        .material
        .SetColor(
          colorId,
          color
        );

      rightWall
        .gameObject
        .GetComponent<Renderer>()
        .material
        .SetColor(
          colorId,
          color
        );
    }

  }
}