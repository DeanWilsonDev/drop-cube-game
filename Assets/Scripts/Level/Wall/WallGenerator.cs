using UnityEngine;

public static class WallGenerator
{
    static GameObject GenerateWall(Component parent, float roomHeight, string wallGameObjectName) {
        var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.localScale = new Vector3(1, roomHeight, 5);
        var parentTransform = parent.transform;
        var parentPosition = parentTransform.position;
        wall.transform.position = new Vector3(
            parentPosition.x,
            parentPosition.y + (wall.transform.localScale.y / 2),
            parentPosition.z
        );
        wall.transform.parent = parentTransform;
        wall.gameObject.name = wallGameObjectName;
        return wall;
    }

    static Vector3 GetRightWallTransformPosition(Component parent, Component rightWall, float totalRoomWidth) {
        var transformPosition = parent.transform.position;
        return new Vector3(
            transformPosition.x + totalRoomWidth,
            rightWall.transform.position.y,
            transformPosition.z
        );
    }
    
    static GameObject GenerateWalls(Component parent, float roomHeight, float totalRoomWidth) {
        
        var leftWall = GenerateWall(parent, roomHeight, "Left Wall");
        var rightWall = GenerateWall(parent, roomHeight, "Right Wall");
        
        var walls = new GameObject {
            name = "Walls"
        };
        
        walls.AddComponent<Walls>();

        rightWall.transform.position = GetRightWallTransformPosition(parent, rightWall.transform, totalRoomWidth);

        leftWall.transform.parent = walls.transform;
        rightWall.transform.parent = walls.transform;
        walls.transform.parent = parent.transform;
        
        var wallComponent = walls.GetComponent<Walls>();
        wallComponent.wallGameObjects.Add(leftWall);
        wallComponent.wallGameObjects.Add(rightWall);
        
        return walls;
    }
    
    public static Walls InitializeWall(Component parent, float roomHeight, float totalRoomWidth) {
        var wall = GenerateWalls(parent, roomHeight, totalRoomWidth)
            .GetComponent<Walls>();
        return wall;
    }
}
