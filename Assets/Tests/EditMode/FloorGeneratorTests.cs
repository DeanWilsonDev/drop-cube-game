using System.Collections.Generic;
using System.Linq;
using BlackPad.DropCube.Level;
using BlackPad.DropCube.Level.Room.Floor;
using NUnit.Framework;
using UnityEngine;
using FluentAssertions;

[TestFixture]
public class FloorGeneratorTests {

  const int FLOOR_OBJECTS_COUNT = 2;
  const float DEFAULT_DOOR_SIZE = 5f;
  const float DEFAULT_ROOM_SIZE = 25f;
  GameObject parent;
  Floor floor;

  [SetUp]
  public void Setup() {
    parent = new GameObject {
      transform = {
        position = new Vector3(0, 0, 0)
      }
    };
  }

  [TearDown]
  public void TearDown() {
    floor = null;
  }

  [Test]
  public void InitializesObjectOfTypeFloor() {
    var floorGenerator = new FloorGenerator(parent.transform, DEFAULT_ROOM_SIZE, DEFAULT_DOOR_SIZE);
    floor = floorGenerator.Initialize();

    floor
      .Should()
      .BeOfType<Floor>();
  }

  [Test]
  public void CreatesFloorObjects() {
    var floorGenerator = new FloorGenerator(parent.transform, DEFAULT_ROOM_SIZE, DEFAULT_DOOR_SIZE);

    floorGenerator
      .Initialize();
    floorGenerator
      .SetupPrefab(null);
    floor = floorGenerator
      .GetGeneratedFloor();

    floor
      .floorGameObjects
      .Should()
      .HaveCount(FLOOR_OBJECTS_COUNT)
      .And.NotContainNulls()
      .And.BeAssignableTo<List<GameObject>>();
  }

  [Test]
  public void CreatesFloorObjectsThatAreFiftyUnitsWide() {
    const float roomWidth = 50f;
    var floorGenerator = new FloorGenerator(parent.transform, roomWidth, DEFAULT_DOOR_SIZE);

    floorGenerator
      .Initialize();
    floorGenerator
      .SetupPrefab(null);
    floor = floorGenerator
      .GetGeneratedFloor();

    var actualRoomWidth =
      floor
        .floorGameObjects
        .Sum(
          floorGameObject =>
            floorGameObject.transform.localScale.x
        )
      + DEFAULT_DOOR_SIZE;

    actualRoomWidth
      .Should()
      .Be(50f);
  }

  [Test]
  public void CreatesFloorObjectsWithRoomForALargeDoor() {
    const float doorSize = 10f;
    var floorGenerator = new FloorGenerator(parent.transform, DEFAULT_ROOM_SIZE, doorSize);
    floorGenerator
      .Initialize();
    floorGenerator
      .SetupPrefab(null);
    floor = floorGenerator
      .GetGeneratedFloor();

    var actualDoorSize =
      floor
        .floorGameObjects
        .Aggregate(
          DEFAULT_ROOM_SIZE,
          (current, floorObjects) =>
            current - floorObjects.transform.localScale.x
        );

    actualDoorSize
      .Should()
      .Be(10f);
  }

  [Test]
  public void SetsFloorObjectPosition() {
    Assert.Inconclusive();
  }

  [Test]
  public void SetsFloorObjectColor() {
    Assert.Inconclusive();
  }

}