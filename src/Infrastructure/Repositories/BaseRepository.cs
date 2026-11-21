using DotNetEnv;
using MySql.Data.MySqlClient;

namespace Videoteket.src.Infrastructure.Repositories;

public abstract class BaseRepository
{
  protected static MySqlConnection CreateConnection()
  {
    Env.Load();

    string connectionString =
      Environment.GetEnvironmentVariable("CONNECTION_STRING")
      ?? throw new InvalidOperationException("Did not find a CONNECTION_STRING!");

    return new MySqlConnection(connectionString);
  }
}
