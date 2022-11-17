using BlackPad.Core.Utilities;
using BlackPad.DropCube.Level.Room.Switch;
using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace BlackPad.DropCube.Level.Room.Wall;

public class RightWallGenerator: Generator<Wall>
{
    const string RightWallName = "Right Wall";
    float _roomHeight;
    float _roomWidth;

    public RightWallGenerator()
    {
        ObjectName = RightWallName;
    }

    public RightWallGenerator Initialize(float roomHeight, float roomWidth)
    {
        _roomHeight = roomHeight;
        _roomWidth = roomWidth;
        return this;
    }
}