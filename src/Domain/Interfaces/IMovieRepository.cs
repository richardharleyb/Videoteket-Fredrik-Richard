namespace Videoteket.src.Domain.Interfaces
{
  public interface IMovieRepository
  {
    public bool MovieExists(string title);
    public void AddMovie(Movie movie);
    public List<Movie> FetchAllMovies();
  }
}
