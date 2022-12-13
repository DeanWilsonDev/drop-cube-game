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
    int layerMask;
    bool leftWallFlag;
    bool rightWallFlag;
    
    public void Initialize() => speed = 75;

    // Start is called before the first frame update
    void Awake() {
      inputManager = InputManager.Instance;
      cameraMain = UnityEngine.Camera.main;
      layerMask = 1 << 6;
      // layerMask = ~layerMask;
    }

    void Start() => Initialize();

    void OnEnable() {
      inputManager.OnTouchStart += Move;
      inputManager.OnTouchEnd += StopMoving;
    }

    void OnDisable() => inputManager.OnTouchStart -= Move;

    bool DetectWallInDirection(Vector3 direction)
    {
      return Physics.Raycast(
        transform.position, 
        transform.TransformDirection(direction), 
        1,
        layerMask
      );
    }

    float GetPlayerMoveDirection(Vector2 screenPosition)
    {
      var direction = screenPosition.x / halfTheWidthOfTheScreen - 1f;
      leftWallFlag = DetectWallInDirection(Vector3.left);
      rightWallFlag = DetectWallInDirection(Vector3.right);

      return direction switch
      {
        > 0 when !rightWallFlag => direction,
        < 0 when !leftWallFlag => direction,
        _ => 0
      };
    }
    
    void Move(Vector2 screenPosition) {
      var position = transform.position;
      var direction = GetPlayerMoveDirection(screenPosition);
      position = Vector3.MoveTowards(
        position, 
        new Vector3(position.x + direction, position.y, position.z), 
        speed * Time.deltaTime
        );
      transform.position = position;
    }


    void StopMoving() {}

  }
}