using BlackPad.Core.Utilities;
using BlackPad.DropCube.Level.Room.Switch;
using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace BlackPad.DropCube.Level.Room.Wall;

public class BackWallGenerator: Generator<BackWall>
{
    const string BackWallName = "Back Wall";
    float _roomHeight;
    float _roomWidth;

    public BackWallGenerator()
    {
        ObjectName = BackWallName;
    }

    public BackWallGenerator Initialize(float roomHeight, float roomWidth)
    {
        _roomHeight = roomHeight;
        _roomWidth = roomWidth;
        return this;
    }

    public override BackWall Generate()
    {
        GeneratedObject = base.Generate();
        GeneratedObject.transform.localScale = new Vector3(
            _roomWidth,
            _roomHeight,
            1
        );
        return GeneratedObject;
    }

    public override void SetPosition()
    {
        GeneratedObject.transform.position = new Vector3(
            Utilities.GameObjectTransformPosition(
                    Parent.gameObject
                )
                .x
            + _roomWidth / 2,
            Utilities.GameObjectTransformPosition(
                    Parent.gameObject
                )
                .y
            + _roomHeight / 2,
            Utilities.GameObjectTransformPosition(
                    Parent.gameObject
                )
                .z
            + 2.5f
        );
    }
}