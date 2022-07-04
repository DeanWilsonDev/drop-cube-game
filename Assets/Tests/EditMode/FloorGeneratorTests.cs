using BlackPad.DropCube.Level;
using NUnit.Framework;
using UnityEngine;
using FluentAssertions;

public class FloorGeneratorTests : MonoBehaviour
{
  public class SetupFloorPortionTests {

    [Test]
    public void ReturnsFloorPortionGameObject() {

      var parent = new GameObject();

      var actual = FloorGenerator.SetupFloorPortion(parent.transform, 5);

      actual
        .Should()
        .BeOfType<GameObject>();
    }
    
    [Test]
    public void ReturnsFloorPortionAtParentPosition() {

      var parent = new GameObject {
        transform = {
          position = new Vector3(100, 500, 1000)
        }
      };

      var actual = FloorGenerator.SetupFloorPortion(parent.transform, 5);

      actual
        .transform
        .position
        .Should()
        .Be(new Vector3(100, 500, 1000));
    }
    
    [Test]
    public void ReturnsFloorPortionOfGivenSize() {

      var parent = new GameObject {
        transform = {
          position = new Vector3(100, 500, 1000)
        }
      };

      var size = 50;

      var actual = FloorGenerator.SetupFloorPortion(parent.transform, size);

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
      var door = new GameObject() {
        transform = {
          localScale = new Vector3(3,1,1)
        }
      };
      var expected = availableSpace - door.transform.localScale.x;
      
      var actual = FloorGenerator.SelectDoorSide(availableSpace, door);
      var actualTotal = actual[0] + actual[1];

      actualTotal
        .Should()
        .Be(expected);
    }
  }
  
  public class ApplyOffsetTests {

    [Test]
    public void ReturnDoorAllocatedFloorPortions() {

      const int availableSpace = 25;
      var floorAllocation = new []{
        12.0f,
        10.0f
      };
      const float doorSize = 3;
      const float offset = 2f;
      var offsetFlags = new[] {
        true, false
      };
      const float expected = availableSpace - doorSize;
      
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
      };
      
      var door = new GameObject() {
        transform = { localScale = new Vector3(3,1,1) }
      };
      
      var actual = FloorGenerator.GenerateFloorObjects(parent.transform, 25, door);

      var actualLength = actual[0]
                           .transform.localScale.x
                         + actual[1]
                           .transform.localScale.x;
      
      actualLength.Should()
        .Be(22);
    }

  }
}
