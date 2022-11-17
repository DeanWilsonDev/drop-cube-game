using BlackPad.Core.Utilities;
using BlackPad.DropCube.Core;
using BlackPad.DropCube.Level.Room;
using JetBrains.Annotations;
using UnityEngine;

namespace BlackPad.DropCube.Level
{
    public class LevelObjectBuilderFactory<TComponent>
        where TComponent : Component
    {
        readonly LevelObjectBuilder<TComponent> _levelObjectBuilder;

        public LevelObjectBuilderFactory()
        {
            _levelObjectBuilder = new LevelObjectBuilder<TComponent>();
        }

        public TComponent Initialize(
            string objectName,
            Component parent,
            Vector3? position,
            Vector3? scale,
            [CanBeNull] GameObject prefab,
            Color? color
        )
        {
            return _levelObjectBuilder.Initialize(
                    objectName,
                    parent,
                    position,
                    scale,
                    prefab,
                    color
                )
                .Generate()
                .SetupPrefab()
                .SetPosition()
                .SetScale()
                .SetColor()
                .GetProduct();
        }
    }
}