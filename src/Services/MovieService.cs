using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Services;

public class MovieService(IMovieRepository movieRepository)
{
  private readonly IMovieRepository _movieRepository = movieRepository;

  public void AddMovie(Movie movie)
  {
    if (_movieRepository.MovieExists(movie.Title))
    {
      throw new InvalidOperationException($"Movie '{movie.Title}' already exists");
    }
    _movieRepository.AddMovie(movie);
  }
}
