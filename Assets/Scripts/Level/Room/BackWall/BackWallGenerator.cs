using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public static class BackWallGenerator
    {

        static readonly int Color1 = Shader.PropertyToID("_Color");

        public static BackWall InitializeBackWall(Component parent, float roomHeight, float totalRoomWidth) {
            var backWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            backWall.name = "Back Wall";
            backWall.transform.localScale = new Vector3(totalRoomWidth, roomHeight, 1);
            var position = parent.transform.position;
            backWall.transform.position = new Vector3(
                position.x + totalRoomWidth / 2,
                position.y + roomHeight / 2,
                position.z + 2.5f
            );
            var renderer = backWall.GetComponent<Renderer>();
            renderer.material.SetColor(Color1, Color.black);
            backWall.AddComponent<BackWall>();
            backWall.transform.parent = parent.transform;
            return backWall.GetComponent<BackWall>();
        }
    }
}
