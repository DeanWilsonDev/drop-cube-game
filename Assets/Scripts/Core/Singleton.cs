using UnityEngine;

namespace BlackPad.DropCube.Core
{
    public class Singleton<T> : MonoBehaviour where T: Component {

        static T _instance;

        public static T Instance {
            get {
                if (_instance != null) return _instance;
                var obj = new GameObject {
                    name = typeof(T).Name,
                    hideFlags = HideFlags.HideAndDontSave
                };
                _instance = obj.AddComponent<T>();
                return _instance;
            }
        }

        void Awake() {
            if (_instance != null && _instance != this) {
                Destroy(this);
            }
            var obj = new GameObject {
                name = typeof(T).Name,
                hideFlags = HideFlags.HideAndDontSave
            };
            _instance = obj.AddComponent<T>();
        }

        void OnDestroy() {
            if (_instance == this) {
                _instance = null;
            }
        }

    }
}