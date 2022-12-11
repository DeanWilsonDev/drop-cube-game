using UnityEngine;

namespace BlackPad.DropCube.Core
{
    public static class ColorAssigner
    {
        public static void AssignColor(GameObject obj, Color color)
        {
            var colorId = Shader.PropertyToID(
                "_Color"
            );

            foreach (
                var renderer in obj
                    .GetComponentsInChildren<Renderer>()
            )
            {
                renderer.material.SetColor(
                    colorId,
                    color
                );
                
                renderer.material.shader = Shader.Find("Unlit/Color");
            }
        }
    }
}