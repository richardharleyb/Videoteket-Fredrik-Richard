using Videoteket.src.Domain;
using Videoteket.src.Services;

namespace Videoteket.Tests;

public class MovieServiceTests
{
  // En film kan inte läggas till två gånger
  [Fact]
  public void A_Movie_Cannot_Be_Added_Twice()
  {
    // Arrange
    var repo = new StubMovieRepository([], movieExists: true);
    var sut = new MovieService(repo);
    var movie = new Movie(title: "Titanic", releaseDate: new DateOnly(1997, 12, 19), isNew: true);

    // Act and Assert
    Assert.Throws<InvalidOperationException>(() => sut.AddMovie(movie));
  }
}
