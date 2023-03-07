using System.Collections.Generic;
using UnityEngine;

namespace BlackPad.DropCube.Data
{
    [CreateAssetMenu(menuName="Scriptable Objects/StringList Variable")]
    public class StringListVariable : ScriptableObject {
        public List<string> value;
    }
}