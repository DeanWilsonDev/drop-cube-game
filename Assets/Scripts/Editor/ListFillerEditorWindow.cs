using System;
using System.IO;
using BlackPad.DropCube.Data;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BlackPad.DropCube.Editor
{
    public class ListFillerEditorWindow : EditorWindow
    {
        Object _fileObject;
        
        StringListVariable _stringList;

        [MenuItem("Window/Custom/ListFiller")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ListFillerEditorWindow), false, "List Filler-uper-er");
        }
        
        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            _fileObject = EditorGUILayout.ObjectField("Select File", _fileObject, typeof(TextAsset), false);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
            _stringList = (StringListVariable)EditorGUILayout.ObjectField("ListObject", _stringList, typeof(ScriptableObject), false);
            EditorGUILayout.EndHorizontal();
            

            if (GUILayout.Button( "Generate List"))
            {
                if (_stringList.value == null)
                {
                    EditorUtility.DisplayDialog(
                        "Select File",
                        "You must select a file first",
                        "OK"
                    );
                    return;
                }
                
                string path = AssetDatabase.GetAssetPath(_fileObject);
                if (path.Length != 0)
                {
                    ReadFile(path);
                }
            }
        }

        void ReadFile(string path)
        {

            if (_stringList.value == null) return;
            if(!File.Exists(path)) Console.WriteLine("File not Found!");
            using var reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                _stringList.value.Add(line);
            }
        }
    }
}
