using System;
using BlackPad.DropCube.Level;
using UnityEngine;

namespace BlackPad.Core
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
                Debug.Log(_instance);
                return _instance;
            }
        }

        void OnDestroy() {
            if (_instance == this) {
                _instance = null;
            }
        }

    }
}
