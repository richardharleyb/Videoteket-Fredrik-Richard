using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.Tests;

public class StubMovieRepository(List<Movie> allMovies, bool movieExists = false) : IMovieRepository
{
  private readonly List<Movie> _allMovies = allMovies;
  private readonly bool _movieExists = movieExists;

  public bool MovieExists(string title)
  {
    return _movieExists;
  }

  public void AddMovie(Movie movie) { }

  public List<Movie> FetchAllMovies()
  {
    return _allMovies;
  }
}
