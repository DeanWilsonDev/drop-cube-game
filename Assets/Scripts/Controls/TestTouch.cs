using System.Collections;
using UnityEngine;

namespace BlackPad.DropCube.Controls {
  public class TestTouch : MonoBehaviour {

    InputManager _inputManager;
    Camera _cameraMain;
    bool _isTouching;

    // Start is called before the first frame update
    void Awake() {
      _inputManager = InputManager.Instance;
      _cameraMain = Camera.main;
    }

    void OnEnable() {
      _inputManager.OnTouchStart += Move;
      _inputManager.OnTouchEnd += StopMoving;
    }

    void OnDisable() => _inputManager.OnTouchStart -= Move;

    void Move(Vector2 screenPosition) {
      var screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, _cameraMain.nearClipPlane);
      var worldCoordinates = _cameraMain.ScreenToWorldPoint(screenCoordinates);
      var destination = new Vector3(worldCoordinates.x, transform.position.y, 0);
      transform.position = destination;
    }

    void StopMoving() {}

  }
}