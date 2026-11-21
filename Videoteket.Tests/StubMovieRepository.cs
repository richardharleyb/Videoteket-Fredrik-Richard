using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.Tests;

public class StubMovieRepository(List<Movie> allMovies, bool movieExists) : IMovieRepository
{
  private readonly List<Movie> _allMovies = allMovies;

  public void AddMovie() { }

  public List<Movie> FetchAllMovies()
  {
    return _allMovies;
  }
}
