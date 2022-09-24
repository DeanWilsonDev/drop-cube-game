using System;
using UnityEngine;

namespace BlackPad.DropCube.Level {

  public abstract class Generator {

    protected Component Parent;
    
    protected T Initialize<T>(string name) where T : Component {
      try {
        if (Parent == null)  throw new Exception("Parent not set to an instance of an object");
        var parentTransform = Parent.transform;
        var generatedObject = new GameObject() {
          name = name,
          transform = {
            position = parentTransform.position,
            parent = parentTransform
          }
        };
        return generatedObject.AddComponent<T>();
      }
      catch (Exception exception) {
        Debug.LogError(exception);
        return null;
      }
    }
    
    protected static void SetupPrefab(GameObject generatedObject, GameObject prefab) {
      try {
        if (prefab == null) throw new NullReferenceException("prefab parameter in <TGenerator>.SetupPrefab not set");

        var prefabInstance = UnityEngine.Object.Instantiate(
          prefab,
          generatedObject.transform.position,
          Quaternion.identity
        );

        generatedObject.AddComponent<BoxCollider>();
        prefabInstance.transform.parent = generatedObject.transform;
      }
      catch (NullReferenceException nullReferenceException) {
        Debug.LogError(nullReferenceException);
      }
    }
  }
}