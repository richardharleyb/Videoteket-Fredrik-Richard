using Videoteket.src.Domain;

namespace Videoteket.Tests;

public class PhysicalCopyTests
{
  //En kopia som är uthyrd kan inte hyras ut
  [Fact]
  public void A_Physical_Copy_Which_Is_Rented_Cannot_Be_Rented()
  {
    //Arrange
    var copy = new PhysicalCopy(barcode: "VHS-EVIL-DEAD-2-01", isActive: true, movieId: 14);
    copy.Rent();

    //Act and Assert
    Assert.Throws<InvalidOperationException>(() => copy.Rent());
  }
}
