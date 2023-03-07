using System;
using System.IO;
using BlackPad.DropCube.Data;
using UnityEditor;
using UnityEngine;

namespace BlackPad.DropCube.Core
{
  public static class ListFiller
  {
    
    [MenuItem("Utilities/Fill String List")]
    public static void FillStringList(MenuCommand menuCommand)
    {
      var stringList = ScriptableObject.CreateInstance<StringListVariable>();
      
      if(!File.Exists(menuCommand.context.name)) Console.WriteLine("File not Found!");
      using var reader = new StreamReader(menuCommand.context.name);
      while (!reader.EndOfStream)
      {
        string line = reader.ReadLine();
        stringList.value.Add(line);
      }
    }
  }
}