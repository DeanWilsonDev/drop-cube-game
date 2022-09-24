using System;
using System.Collections;
using BlackPad.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BlackPad.DropCube.Controls {
  public class InputManager : Singleton<InputManager> {

    public delegate void StartTouchEvent(Vector2 position);
    public event StartTouchEvent OnTouchStart;

    public delegate void EndTouchEvent();
    public event EndTouchEvent OnTouchEnd;

    bool isTouching;
    TouchControls touchControls;

    void Awake() => touchControls = new TouchControls();

    void OnEnable() => touchControls.Enable();

    void OnDisable() => touchControls.Disable();

    void Start() {
      touchControls.Touch.TouchInput.performed += StartTouch;
      touchControls.Touch.TouchInput.canceled += EndTouch;
    }

    IEnumerator HoldTouch() {
      while (isTouching) {
        var screenPosition = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        OnTouchStart?.Invoke(
          screenPosition
        );
        yield return null;
      }
    }

    void StartTouch(InputAction.CallbackContext context) {
      isTouching = true;
      StartCoroutine(HoldTouch());
    }

    void EndTouch(InputAction.CallbackContext context) {
      if (!isTouching) return;
      OnTouchEnd?.Invoke();
      isTouching = false;
    }

  }
}