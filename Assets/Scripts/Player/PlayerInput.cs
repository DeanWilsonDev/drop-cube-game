using BlackPad.DropCube.Controls;
using UnityEngine;

namespace BlackPad.DropCube.Player {
  public class PlayerInput : MonoBehaviour {

    InputManager _inputManager;
    UnityEngine.Camera _cameraMain;
    bool _isTouching;
    float _speed; 
    

    public void Initialize() => _speed = 50;

    // Start is called before the first frame update
    void Awake() {
      _inputManager = InputManager.Instance;
      _cameraMain = UnityEngine.Camera.main;
    }

    void Start() => Initialize();

    void OnEnable() {
      _inputManager.OnTouchStart += Move;
      _inputManager.OnTouchEnd += StopMoving;
    }

    void OnDisable() => _inputManager.OnTouchStart -= Move;

    void Move(Vector2 screenPosition) {
      var screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, _cameraMain.nearClipPlane);
      var worldCoordinates = _cameraMain.ScreenToWorldPoint(screenCoordinates);
      var position = transform.position;
      var destination = new Vector3(worldCoordinates.x, position.y, 0);
      position = Vector3.MoveTowards(position, destination, _speed * Time.deltaTime);
      transform.position = position;
    }

    void StopMoving() {}

  }
}