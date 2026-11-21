using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Services;

public class MovieService(IMovieRepository movieRepository)
{
  private readonly IMovieRepository _movieRepository = movieRepository;

  public void AddMovie(Movie movie) { }
}
