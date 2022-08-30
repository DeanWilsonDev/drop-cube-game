using UnityEngine;

namespace BlackPad.DropCube.Level {

  public class Room : MonoBehaviour {

    GameObject _leftSide;
    GameObject _rightSide;
    [SerializeField] float doorSize = 5f;
    [SerializeField] Walls walls;
    [SerializeField] BackWall backWall;
    [SerializeField] Floor floor;
    [SerializeField] Door door;
    [SerializeField] float roomHeight = 10f;
    [SerializeField] float roomWidth = 25;
    public float RoomHeight {
      get => roomHeight;
      set => roomHeight = value;
    }
    public float RoomWidth {
      get => roomWidth;
      set => roomWidth = value;
    }

    
    void Start() => SetupRoom();

    void SetupRoom() {
      SetupDoor();
      SetupFloor(roomWidth);
      SetupWalls();
    }

    void SetupDoor() => door = DoorGenerator.InitializeDoor(transform, doorSize);

    void SetupFloor(float availableSpace) =>
      floor = FloorGenerator.InitializeFloor(transform, availableSpace, door.gameObject);

    void SetupWalls() {
      walls = WallGenerator.InitializeWall(transform, roomHeight, roomWidth);
      backWall = BackWallGenerator.InitializeBackWall(transform, roomHeight, roomWidth);
    }
  }
}