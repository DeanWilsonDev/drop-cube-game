using BlackPad.DropCube.Controls;
using BlackPad.DropCube.Level;
using UnityEngine;

namespace BlackPad.DropCube.Player
{
  public class PlayerInput : MonoBehaviour
  {

    InputManager _inputManager;
    bool _isTouching;
    float _speed;
    readonly float _halfTheWidthOfTheScreen = Screen.width / 2f;
    int _layerMask;
    bool _leftWallFlag;
    bool _rightWallFlag;
    const string GameManagerTag = "GameManager";
    
    public void Initialize() => _speed = 75;

    // Start is called before the first frame update
    void Awake() {
      _layerMask = 1 << 6;
      // layerMask = ~layerMask;
      _inputManager = GameObject.FindGameObjectWithTag(GameManagerTag).GetComponent<InputManager>();
    }

    void Start() => Initialize();

    void OnEnable() {
      _inputManager.OnTouchStart += Move;
      _inputManager.OnTouchEnd += StopMoving;
    }

    void OnDisable() => _inputManager.OnTouchStart -= Move;

    bool DetectWallInDirection(Vector3 direction)
    {
      return Physics.Raycast(
        transform.position, 
        transform.TransformDirection(direction), 
        1,
        _layerMask
      );
    }

    float GetPlayerMoveDirection(Vector2 screenPosition)
    {
      var direction = screenPosition.x / _halfTheWidthOfTheScreen - 1f;
      _leftWallFlag = DetectWallInDirection(Vector3.left);
      _rightWallFlag = DetectWallInDirection(Vector3.right);

      return direction switch
      {
        > 0 when !_rightWallFlag => direction,
        < 0 when !_leftWallFlag => direction,
        _ => 0
      };
    }
    
    void Move(Vector2 screenPosition) {
      var position = transform.position;
      var direction = GetPlayerMoveDirection(screenPosition);
      position = Vector3.MoveTowards(
        position, 
        new Vector3(position.x + direction, position.y, position.z), 
        _speed * Time.deltaTime
      );
      transform.position = position;
    }


    void StopMoving() {}

  }
}