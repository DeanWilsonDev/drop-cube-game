using System;
using BlackPad.DropCube.Core;
using UnityEngine;

namespace BlackPad.DropCube.Level {

  public abstract class Generator<TComponent> : IGenerator<TComponent>
    where TComponent : Component
  {

    protected Component Parent { get; set; }
    protected string ObjectName { get; set; }
    
    protected TComponent GeneratedObject { get; set; }

    public virtual TComponent Generate() {
      try {
        if (Parent == null)  throw new NullReferenceException("Parent not set to an instance of an object");
        var parentTransform = Parent.transform;
        var generatedGameObject = new GameObject {
          name = ObjectName,
          transform = {
            position = parentTransform.position,
            parent = parentTransform
          }
        };
        GeneratedObject = generatedGameObject.AddComponent<TComponent>();
        return GeneratedObject;
      }
      catch (Exception exception) {
        Debug.LogError(exception);
        return null;
      }
    }
    
    public virtual void SetupPrefab(GameObject prefab) {
      try {
        if (prefab == null) throw new NullReferenceException("prefab parameter in <TGenerator>.SetupPrefab not set");

        var prefabInstance = UnityEngine.Object.Instantiate(
          prefab,
          GeneratedObject.transform.position,
          Quaternion.identity
        );
        
        prefabInstance.transform.parent = GeneratedObject.transform;
      }
      catch (NullReferenceException nullReferenceException) {
        Debug.LogError(nullReferenceException);
      }
    }

    public virtual void SetPosition(Vector3? position)
    {
      GeneratedObject.transform.position = position ?? Parent.transform.position;
    }
    
    public virtual void SetScale(Vector3 scale)
    {
      GeneratedObject.transform.localScale = scale;
    }

    public virtual void SetColor(Color color)
    {
      var colorId = Shader.PropertyToID(
        "_Color"
      );
      GeneratedObject
        .GetComponent<Renderer>()
        .material
        .SetColor(
          colorId,
          color
        );
    }
  }
}