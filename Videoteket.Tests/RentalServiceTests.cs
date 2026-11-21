using Videoteket.src.Domain;
using Videoteket.src.Services;

namespace Videoteket.Tests;

public class RentalServiceTests
{
  //En kopia som är uthyrd kan inte hyras ut
  [Fact]
  public void A_Physical_Copy_Which_Is_Rented_Cannot_Be_Rented()
  {
    //Arrange
    var repo = new StubPhysicalCopyRepository([], isRented: true);
    var sut = new RentalService(repo);

    //Act and Assert
    Assert.Throws<InvalidOperationException>(() => sut.Rent("VHS-EVIL-DEAD-2-01"));
  }
}
