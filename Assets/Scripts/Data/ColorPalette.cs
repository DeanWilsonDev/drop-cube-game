using System.Collections.Generic;
using UnityEngine;

namespace BlackPad.DropCube.Data
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Color Palette")]
    public class ColorPalette : ScriptableObject {
        public List<Color> value;
    }
}
