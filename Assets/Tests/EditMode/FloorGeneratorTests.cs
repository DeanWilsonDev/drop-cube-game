using BlackPad.DropCube.Level;
using NUnit.Framework;
using UnityEngine;
using FluentAssertions;

public class FloorGeneratorTests : MonoBehaviour
{
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
  public class IsRandomlySelectedTests {
    
    
    [Test]
    public void ZeroShouldBeTrue() {
      var actual = FloorGenerator.IsRandomlySelected(0);
      actual
        .Should()
        .Be(true);
    }

    [Test]
    public void OnHundredShouldBeFalse() {
      var actual = FloorGenerator.IsRandomlySelected(100);
      actual
        .Should()
        .Be(false);
    }
    
    [Test]
    public void FiftyShouldBeFalse() {
      var actual = FloorGenerator.IsRandomlySelected(50);
      actual
        .Should()
        .Be(false);
    }
    
    [Test]
    public void FortyNineShouldBeTrue() {
      var actual = FloorGenerator.IsRandomlySelected(49);
      actual
        .Should()
        .Be(true);
    }
  }

  public class GetValueIfTrueTests {
    
    
    [Test]
    public void ReturnsZeroIfNotSelected() {
      
      var actual = FloorGenerator.GetValueIfTrue(false , 3);

      actual
        .Should()
        .Be(0);
    }
    
    [Test]
    public void ReturnsNegativeDifferenceIfSelected() {
      
      var actual = FloorGenerator.GetValueIfTrue(true, 3);
      
      actual
        .Should()
        .Be(3);
    }

  }

  public class SelectDoorSideTests {

    

    
    [Test]
    public void ReturnDoorAllocatedFloorPortions() {
      
      const int availableSpace = 25;
      const float doorSize = 3;
      var room = new GameObject() {
      }.AddComponent<Room>();
      
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


      var floorAllocation = new []{
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

  public class GenerateFloorObjectsTests {
    
    [Test]
    public void ReturnsFloorsObjectsWithDoor() {


      var parent = new GameObject() {
        transform = { position = Vector3.zero }
      }.AddComponent<Room>();
      const float availableSpace = 25;
      const float doorSize = 3;

      var floorGenerator = new FloorGenerator(parent, availableSpace, doorSize);
      
      var actual = floorGenerator.GenerateFloorObjects();

      var actualLength = actual[0]
                           .transform.localScale.x
                         + actual[1]
                           .transform.localScale.x;
      
      actualLength.Should()
        .Be(22);
    }

  }
}
