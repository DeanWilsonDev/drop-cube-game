using System;
using BlackPad.DropCube.Core;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BlackPad.DropCube.Level {
  public class LevelObjectBuilder<TComponent> 
    : ILevelObjectBuilder<TComponent>
    where TComponent : Component
  {
    Component _parent;
    string _productName;
    TComponent _product;
    GameObject _generatedObject;
    Vector3? _position;
    Vector3? _scale;
    [CanBeNull] GameObject _prefab;
    Color? _color;

    public ILevelObjectBuilder<TComponent> Initialize(
      string productName,
      Component parent,
      Vector3? position,
      Vector3? scale,
      GameObject prefab,
      Color? color
      )
    {
      _parent = parent;
      _productName = productName;
      _position = position;
      _scale = scale;
      _prefab = prefab;
      _color = color;
      
      return this;
    }
    
    public ILevelObjectBuilder<TComponent> 
      GeneratePrimitiveObject() {
      try {
        
        if (_parent == null)
          throw new NullReferenceException(
            "Parent not set to an instance of an object"
          );
        
        _generatedObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _generatedObject.transform.parent = _parent.transform;
        _generatedObject.name = _productName;
        return this;
      }
      catch (Exception exception) {
        Debug.LogError(exception);
        return this;
      }
    }
    
    public ILevelObjectBuilder<TComponent> 
      GenerateEmptyObject() {
      try {
        
        if (_parent == null)
          throw new NullReferenceException(
            "Parent not set to an instance of an object"
            );
        
        var parentTransform = _parent.transform;
        
        _generatedObject = new GameObject {
          name = _productName,
          transform = {
            position = parentTransform.position,
            localScale = parentTransform.localScale,
            parent = parentTransform
          }
        };
        
        return this;
      }
      catch (Exception exception) {
        Debug.LogError(exception);
        return this;
      }
    }
    
    public ILevelObjectBuilder<TComponent>
      GeneratePrefabObject() {
      try {
        if (_prefab == null)
          throw new NullReferenceException(
            $"prefab parameter in {this}.SetupPrefab not set"
          );

        _generatedObject = Object.Instantiate(
          _prefab,
          _parent.transform.position,
          Quaternion.identity
        );
        
        _generatedObject.transform.parent = _parent.transform;
        _generatedObject.name = _productName;
        
        return this;
      }
      catch (NullReferenceException nullReferenceException) {
        Debug.LogError(nullReferenceException);
        return this;
      }
    }

    public ILevelObjectBuilder<TComponent>
      SetPosition() {
      _generatedObject.transform.position = _position ?? _parent.transform.position;
      return this;
    }

    public ILevelObjectBuilder<TComponent>
      AddComponent()
    {
      _product = _generatedObject.AddComponent<TComponent>();
      return this;
    }

    public ILevelObjectBuilder<TComponent>
      SetScale()
    {
      _generatedObject.transform.localScale = _scale ?? _parent.transform.localScale;
      return this;
    }
    
    public ILevelObjectBuilder<TComponent> 
      SetColor()
    {
      var colorId = Shader.PropertyToID(
        "_Color"
      );

      foreach (
        var renderer in _generatedObject
                 .GetComponentsInChildren<Renderer>()
              )
      {
        renderer.material.SetColor(
              colorId,
              _color ?? Color.white
            );
      }
      return this;
    }

    public GameObject GetGeneratedObject()
    {
      return _generatedObject;
    }

    public TComponent GetProduct() {
      return _product;
    }
  }
}