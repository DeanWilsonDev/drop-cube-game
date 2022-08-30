using System;
using BlackPad.Core;
using BlackPad.DropCube.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackPad.DropCube.Camera {
  public class CameraManager : MonoBehaviour {

    [SerializeField] float maxSpeed;
    GameObject _player;
    [SerializeField] float smoothTime;
    [SerializeField] float threshold;
    Vector3 _velocity = Vector3.zero;
    UnityEngine.Camera _camera;

    void Initialize() {
      maxSpeed = 7;
      smoothTime = 0.1f;
      threshold = 1f;
    }

    void Awake() {
      Initialize();
    }

    void Start() {
      _player = GameObject
        .FindGameObjectWithTag("GameManager")
        .GetComponent<PlayerManager>()
        .player;
      _camera = UnityEngine.Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate() {
      var playerTransformPosition = _player.transform.position;
      var bottomOfTheScreen =
        _camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
      var cameraTransform = _camera.transform;
      var cameraTransformPosition = cameraTransform.position;
      var targetPosition = new Vector3(
        cameraTransformPosition.x,
        bottomOfTheScreen.y - threshold,
        cameraTransformPosition.z
      );

      var topOfScreen = _camera.ScreenToWorldPoint(new Vector3(0, Screen.height, _camera.nearClipPlane));
      var distanceFromTopOfScreen = topOfScreen.y - playerTransformPosition.y;
      transform.position = Vector3.SmoothDamp(
        transform.position,
        targetPosition,
        ref _velocity,
        smoothTime,
        maxSpeed + distanceFromTopOfScreen
      );
      if (!(playerTransformPosition.y >= topOfScreen.y + threshold)) return;
      SceneManager.LoadScene(
        SceneManager.GetActiveScene()
          .name
      );
    }

  }
}