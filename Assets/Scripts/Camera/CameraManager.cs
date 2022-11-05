using BlackPad.DropCube.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackPad.DropCube.Camera {
  public class CameraManager : MonoBehaviour {

    [SerializeField] float maxSpeed;
    GameObject player;
    [SerializeField] float smoothTime;
    [SerializeField] float threshold;
    Vector3 velocity = Vector3.zero;
    UnityEngine.Camera mainCamera;

    void Initialize() {
      maxSpeed = 7;
      smoothTime = 0.1f;
      threshold = 1f;
    }

    void Awake() {
      Initialize();
    }

    void Start() {
      player = GameObject
        .FindGameObjectWithTag("GameManager")
        .GetComponent<PlayerManager>()
        .player;
      mainCamera = UnityEngine.Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate() {
      var playerTransformPosition = player.transform.position;
      var bottomOfTheScreen =
        mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
      var cameraTransform = mainCamera.transform;
      var cameraTransformPosition = cameraTransform.position;
      var targetPosition = new Vector3(
        cameraTransformPosition.x,
        bottomOfTheScreen.y - threshold,
        cameraTransformPosition.z
      );
      var topOfScreen = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, mainCamera.nearClipPlane));
      var distanceFromTopOfScreen = topOfScreen.y - playerTransformPosition.y;
      
      transform.position = MoveCameraDown(targetPosition, distanceFromTopOfScreen);
      if (!(playerTransformPosition.y >= topOfScreen.y + threshold)) return;
      KillPlayer();
    }

    static void KillPlayer() {
      SceneManager.LoadScene(
        SceneManager.GetActiveScene()
          .name
      );
    }

    Vector3 MoveCameraDown(Vector3 targetPosition, float distanceFromTopOfScreen) {
      var position = transform.position;
      return Vector3.SmoothDamp(
        position,
        new Vector3(position.x, targetPosition.y, position.z),
        ref velocity,
        smoothTime,
        maxSpeed + distanceFromTopOfScreen
      );
    }
  }
}