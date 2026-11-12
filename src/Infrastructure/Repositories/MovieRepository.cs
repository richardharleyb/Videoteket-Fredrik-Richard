using System.Data;
using Dapper;
using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Infrastructure.Repositories;

internal class MovieRepository : BaseRepository, IMovieRepository
{
  public List<Movie> FetchAllMovies()
  {
    using IDbConnection connection = CreateConnection();

    List<Movie> movies =
    [
      .. connection.Query<Movie>("SELECT Id, Title, ReleaseDate, IsNew FROM Movie;"),
    ];

    return movies;
  }
}
