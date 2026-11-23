using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;
using Videoteket.src.Infrastructure.Repositories;

namespace Videoteket.src.Services;

public class MovieService(IMovieRepository movieRepository) // for tests: send in fake
{
  private readonly IMovieRepository _movieRepository = movieRepository;

  public MovieService()
    : this(new MovieRepository()) { } // for menu, uses the real one

  public void AddMovie(Movie movie)
  {
    if (_movieRepository.MovieExists(movie.Title))
    {
      throw new InvalidOperationException($"Movie '{movie.Title}' already exists");
    }
    _movieRepository.AddMovie(movie);
  }

  public List<Movie> FetchAllMovies()
  {
    return _movieRepository.FetchAllMovies();
  }
}
