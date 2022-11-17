using System;
using BlackPad.DropCube.Core;
using JetBrains.Annotations;
using UnityEngine;

namespace BlackPad.DropCube.Level {
  public class LevelObjectBuilder<TComponent> 
    : ILevelObjectBuilder<TComponent>
    where TComponent : Component
  {
    Component _parent;
    string _productName;
    TComponent _product;
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
      
      Reset();
      return this;
    }

    void Reset() {
      _product = GetProduct();
    }

    public ILevelObjectBuilder<TComponent> 
      Generate() {
      try {
        
        if (_parent == null)
          throw new NullReferenceException(
            "Parent not set to an instance of an object"
            );
        
        var parentTransform = _parent.transform;
        
        var generatedGameObject = new GameObject {
          name = _productName,
          transform = {
            position = parentTransform.position,
            parent = parentTransform
          }
        };
        
        _product = generatedGameObject.AddComponent<TComponent>();
        
        return this;
      }
      catch (Exception exception) {
        Debug.LogError(exception);
        return this;
      }
    }

    public ILevelObjectBuilder<TComponent>
      SetupPrefab() {
      try {
        if (_prefab == null)
          throw new NullReferenceException(
            $"prefab parameter in {this}.SetupPrefab not set"
            );

        var prefabInstance = UnityEngine.Object.Instantiate(
          _prefab,
          _product.transform.position,
          Quaternion.identity
        );
        
        prefabInstance.transform.parent = _product.transform;
        
        return this;
      }
      catch (NullReferenceException nullReferenceException) {
        Debug.LogError(nullReferenceException);
        return this;
      }
    }

    public ILevelObjectBuilder<TComponent>
      SetPosition() {
      _product.transform.position = _position ?? _parent.transform.position;
      return this;
    }

    public ILevelObjectBuilder<TComponent>
      SetScale()
    {
      _product.transform.localScale = _scale ?? _parent.transform.localScale;
      return this;
    }
    
    public ILevelObjectBuilder<TComponent> 
      SetColor()
    {
      var colorId = Shader.PropertyToID(
        "_Color"
      );
      _product
        .GetComponent<Renderer>()
        .material
        .SetColor(
          colorId,
          _color ?? Color.white
        );

      return this;
    }

    public TComponent GetProduct() {
      return _product;
    }
  }
}