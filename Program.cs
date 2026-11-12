using Videoteket.src.Infrastructure;

namespace Videoteket;

internal static class Program
{
  internal static void Main(string[] args)
  {
    DatabaseSeeder.Seed();
  }
}
