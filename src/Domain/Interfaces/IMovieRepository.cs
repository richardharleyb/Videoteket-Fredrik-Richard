namespace Videoteket.src.Domain.Interfaces
{
  public interface IMovieRepository
  {
    public void AddMovie();
    public List<Movie> FetchAllMovies();
  }
}
