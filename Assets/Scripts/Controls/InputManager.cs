using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BlackPad.DropCube.Controls
{
  public class InputManager: MonoBehaviour {

    public delegate void StartTouchEvent(Vector2 position);
    public event StartTouchEvent OnTouchStart;

    public delegate void EndTouchEvent();
    public event EndTouchEvent OnTouchEnd;

    bool _isTouching;
    TouchControls _touchControls;

    void Awake() => _touchControls = new TouchControls();

    void OnEnable() => _touchControls.Enable();

    void OnDisable() => _touchControls.Disable();

    void Start() {
      _touchControls.Touch.TouchInput.performed += StartTouch;
      _touchControls.Touch.TouchInput.canceled += EndTouch;
    }

    IEnumerator HoldTouch() {
      while (_isTouching) {
        var screenPosition = _touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        OnTouchStart?.Invoke(
          screenPosition
        );
        yield return null;
      }
    }

    void StartTouch(InputAction.CallbackContext context) {
      _isTouching = true;
      StartCoroutine(HoldTouch());
    }

    void EndTouch(InputAction.CallbackContext context) {
      if (!_isTouching) return;
      OnTouchEnd?.Invoke();
      _isTouching = false;
    }

  }
}