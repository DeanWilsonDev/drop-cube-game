using UnityEngine;

namespace BlackPad.DropCube.Core
{
    public interface IGenerator<out TComponent> where TComponent : Component
    {
        TComponent Initialize();
        
        void SetupPrefab(GameObject prefab);

        void SetPosition();

        void SetColor(Color color);

    }
}
