using BlackPad.DropCube.Controls;
using BlackPad.DropCube.Level;
using UnityEngine;

namespace BlackPad.DropCube.Player {
  public class PlayerInput : MonoBehaviour {

    InputManager inputManager;
    LevelManager levelManager;
    UnityEngine.Camera cameraMain;
    bool isTouching;
    float speed;
    readonly float halfTheWidthOfTheScreen  = Screen.width / 2f;
    
    public void Initialize() => speed = 50;

    // Start is called before the first frame update
    void Awake() {
      inputManager = InputManager.Instance;
      cameraMain = UnityEngine.Camera.main;
    }

    void Start() => Initialize();

    void OnEnable() {
      inputManager.OnTouchStart += Move;
      inputManager.OnTouchEnd += StopMoving;
    }

    void OnDisable() => inputManager.OnTouchStart -= Move;

    bool DetectWallInDirection(Vector3 direction)
    {
      return Physics.Raycast(transform.position, direction, 1);
    }

    float GetPlayerMoveDirection(Vector2 screenPosition)
    {
      return DetectWallInDirection(Vector3.left) || DetectWallInDirection(Vector3.right)
        ? 0f
        : screenPosition.x / halfTheWidthOfTheScreen - 1f;
    }
    
    void Move(Vector2 screenPosition) {
      var position = transform.position;
      var direction = GetPlayerMoveDirection(screenPosition);
      position = Vector3.MoveTowards(position, new Vector3(position.x + direction, position.y, position.z), speed * Time.deltaTime);
      transform.position = position;
    }


    void StopMoving() {}

  }
}