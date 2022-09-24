using UnityEngine;

namespace BlackPad.DropCube.Level {

  public class Room : MonoBehaviour {

    GameObject _leftSide;
    GameObject _rightSide;
    FloorGenerator floorGenerator;
    [SerializeField] Walls walls;
    [SerializeField] BackWall backWall;
    [SerializeField] Floor floor;
    [SerializeField] Door door;
    [SerializeField] float roomHeight ;
    [SerializeField] float roomWidth;
    
    public float RoomHeight {
      get => roomHeight;
      set => roomHeight = value;
    }
    
    public float RoomWidth {
      get => roomWidth;
      set => roomWidth = value;
    }

    
    void Start() {
      floorGenerator = new FloorGenerator(transform, roomWidth, RoomManager.Instance.DoorSize);
      SetupRoom();
    }

    void SetupRoom() {
      SetupFloor(roomWidth);
      SetupWalls();
    }


    void SetupFloor(float availableSpace) =>
      floor = floorGenerator.InitializeFloor();

    void SetupWalls() {
      walls = WallGenerator.InitializeWall(transform, roomHeight, roomWidth);
      backWall = BackWallGenerator.InitializeBackWall(transform, roomHeight, roomWidth);
    }
  }
}