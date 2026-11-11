namespace Videoteket.src.Domain.Interfaces
{
  internal interface IMovieRepository
  {
    public List<Movie> FetchAllMovies();
  }
}
