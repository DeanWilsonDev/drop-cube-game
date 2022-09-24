using BlackPad.DropCube.Level;
using NUnit.Framework;
using UnityEngine;
using FluentAssertions;

[TestFixture]
public class FloorGeneratorTests {

  [TestFixture]
  public class SetupFloorPortionTests {

    FloorGenerator floorGenerator;

    [SetUp]
    public void SetUp() {
      var parent = new GameObject {
        transform = {
          position = new Vector3(100, 500, 1000)
        }
      }.AddComponent<Room>();

      const int availableSpace = 25;
      const float doorSize = 3;
      floorGenerator = new FloorGenerator(parent, availableSpace, doorSize);
    }

    [Test]
    public void ReturnsFloorPortionGameObject() {
      const float size = 5;

      var actual = floorGenerator.SetupFloorPortion(size);

      actual
        .Should()
        .BeOfType<GameObject>();
    }

    [Test]
    public void ReturnsFloorPortionAtParentPosition() {
      const float size = 5;

      var actual = floorGenerator.SetupFloorPortion(size);

      actual
        .transform
        .position
        .Should()
        .Be(new Vector3(100, 500, 1000));
    }

    [Test]
    public void ReturnsFloorPortionOfGivenSize() {
      const float size = 50;

      var actual = floorGenerator.SetupFloorPortion(size);

      actual
        .transform
        .localScale
        .Should()
        .Be(new Vector3(size, 1, 5));
    }

  }

  public class SelectDoorSideTests {

    [Test]
    public void ReturnDoorAllocatedFloorPortions() {
      const int availableSpace = 25;
      const float doorSize = 3;
      var room = new GameObject() { }.AddComponent<Room>();

      var floorGenerator = new FloorGenerator(room, availableSpace, doorSize);

      const int expected = 22;

      var actual = floorGenerator.SelectDoorSide();
      var actualTotal = actual[0] + actual[1];

      actualTotal
        .Should()
        .Be(expected);
    }

  }

  public class ApplyOffsetTests {

    [Test]
    public void ReturnDoorAllocatedFloorPortions() {
      var floorAllocation = new[] {
        12.0f,
        10.0f
      };
      const float offset = 2f;
      var offsetFlags = new[] {
        true, false
      };

      const float expected = 22; // Available Space = 25 - Door Size = 5

      var actual = FloorGenerator.ApplyOffset(offset, offsetFlags, floorAllocation);
      var actualTotal = actual[0] + actual[1];

      actualTotal
        .Should()
        .Be(expected);
    }
  }
}