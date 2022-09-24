using System;
using BlackPad.DropCube.Level;
using UnityEngine;

namespace BlackPad.Core
{
    public class Singleton<T> : MonoBehaviour where T: Component {

        static T instance;

        public static T Instance {
            get {
                if (instance != null) return instance;
                var obj = new GameObject {
                    name = typeof(T).Name,
                    hideFlags = HideFlags.HideAndDontSave
                };
                instance = obj.AddComponent<T>();
                return instance;
            }
        }

        void Awake() {
            if (instance != null && instance != this) {
                Destroy(this);
            }
            var obj = new GameObject {
                name = typeof(T).Name,
                hideFlags = HideFlags.HideAndDontSave
            };
            instance = obj.AddComponent<T>();
        }

        void OnDestroy() {
            if (instance == this) {
                instance = null;
            }
        }

    }
}
