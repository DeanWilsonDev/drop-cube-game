using UnityEngine;

namespace BlackPad.DropCube.Level {

  public class Room : MonoBehaviour {

    GameObject _leftSide;
    GameObject _rightSide;
    [SerializeField] float totalRoomWidth = 25;
    [SerializeField] float doorSize = 5f;
    [SerializeField] Walls walls;
    [SerializeField] Floor floor;
    [SerializeField] Door door;
    [SerializeField] float roomHeight = 10f;

    public float RoomHeight => roomHeight;
    void Start() => SetupRoom();

    void SetupRoom() {
      SetupDoor();
      SetupFloor(totalRoomWidth);
      SetupWalls();
    }

    void SetupDoor() => door = DoorGenerator.InitializeDoor(transform, doorSize);

    void SetupFloor(float availableSpace) =>
      floor = FloorGenerator.InitializeFloor(transform, availableSpace, door.gameObject);

    void SetupWalls() => walls = WallGenerator.InitializeWall(transform, roomHeight, totalRoomWidth);

  }
}