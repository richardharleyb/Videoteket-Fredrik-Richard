using System.Data;
using Dapper;
using DotNetEnv;
using MySql.Data.MySqlClient;
using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Infrastructure.Repositories;

internal class MovieRepository : IMovieRepository
{
  private static MySqlConnection CreateConnection()
  {
    Env.Load();

    string connectionString =
      Environment.GetEnvironmentVariable("CONNECTION_STRING")
      ?? throw new InvalidOperationException("Did not find a CONNECTION_STRING!");

    return new MySqlConnection(connectionString);
  }

  public List<Movie> FetchAllMovies()
  {
    using IDbConnection connection = CreateConnection();

    List<Movie> movies =
    [
      .. connection.Query<Movie>("SELECT Id, Title, ReleaseDate, IsNew FROM Movie"),
    ];

    return movies;
  }
}
