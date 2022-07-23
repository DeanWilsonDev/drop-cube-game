using System;
using UnityEngine;

namespace BlackPad.DropCube.Camera {
  public class CameraController : MonoBehaviour {

    [SerializeField] float speed;
    GameObject _player;

    UnityEngine.Camera _camera;

    // Start is called before the first frame update
    void Start() {
      _player = GameObject.FindWithTag("Player");
      _camera = GetComponent<UnityEngine.Camera>();
    }

    // Update is called once per frame
    void Update() {
      var playerTransformPosition = _player.transform.position;
      var topOfScreenInWorldPosition =
        _camera.ScreenToWorldPoint(new Vector3(0, Screen.height, _camera.nearClipPlane));
      Debug.Log("camera" + topOfScreenInWorldPosition);
      Debug.Log("Player" + playerTransformPosition);
      var distanceFromBottom = topOfScreenInWorldPosition.y - playerTransformPosition.y;
      transform.Translate(Vector3.down * ((distanceFromBottom + speed) * Time.deltaTime));
    }

  }
}