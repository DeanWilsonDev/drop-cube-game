using System;
using UnityEngine;

namespace BlackPad.DropCube.Camera {
  public class CameraController : MonoBehaviour {

    [SerializeField] float speed;
    GameObject _player;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float threshold;
    Vector3 velocity = Vector3.zero;
    UnityEngine.Camera _camera;

    // Start is called before the first frame update
    void Start() {
      _player = GameObject.FindWithTag("Player");
      _camera = UnityEngine.Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate() {
      var playerTransformPosition = _player.transform.position;
      var bottomOfTheScreen =
        _camera.ScreenToWorldPoint(new Vector3(0, 0, _camera.nearClipPlane));
      var cameraTransform = _camera.transform;
      var cameraTransformPosition = cameraTransform.position;
      var targetPosition = new Vector3(cameraTransformPosition.x, bottomOfTheScreen.y, cameraTransformPosition.z);

      var topOfScreen = _camera.ScreenToWorldPoint(new Vector3(0, Screen.height, _camera.nearClipPlane));  
      var distanceFromTopOfScreen = topOfScreen.y - playerTransformPosition.y;
      _camera.transform.position = Vector3.SmoothDamp(cameraTransformPosition, targetPosition, ref velocity, smoothTime, speed + distanceFromTopOfScreen);

      if (playerTransformPosition.y >= bottomOfTheScreen.y) {
        // Restart the game somehow
      }
    }

  }
}