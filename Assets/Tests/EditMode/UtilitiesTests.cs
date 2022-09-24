using BlackPad.Core.Utilities;
using NUnit.Framework;
using FluentAssertions;

[TestFixture]
public class UtilitiesTests {
  
  [TestFixture]
  public class IsRandomlySelectedTests {
    
    
    [Test]
    public void ZeroShouldBeTrue() {
      var actual = Utilities.IsRandomlySelected(0);
      actual
        .Should()
        .Be(true);
    }

    [Test]
    public void OnHundredShouldBeFalse() {
      var actual = Utilities.IsRandomlySelected(100);
      actual
        .Should()
        .Be(false);
    }
    
    [Test]
    public void FiftyShouldBeFalse() {
      var actual = Utilities.IsRandomlySelected(50);
      actual
        .Should()
        .Be(false);
    }
    
    [Test]
    public void FortyNineShouldBeTrue() {
      var actual = Utilities.IsRandomlySelected(49);
      actual
        .Should()
        .Be(true);
    }
  }

  [TestFixture]
  public class GetValueIfTrueTests {
    
    
    [Test]
    public void ReturnsZeroIfNotSelected() {
      
      var actual = Utilities.GetValueIfTrue(false , 3);

      actual
        .Should()
        .Be(0);
    }
    
    [Test]
    public void ReturnsNegativeDifferenceIfSelected() {
      
      var actual = Utilities.GetValueIfTrue(true, 3);
      
      actual
        .Should()
        .Be(3);
    }
  }
}