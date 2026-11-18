using Videoteket.src.Infrastructure;
using Videoteket.src.UI;

namespace Videoteket;

internal static class Program
{
  internal static void Main(string[] args)
  {
    DatabaseSeeder.Seed();
    MainMenu mainMenu = new MainMenu();
    mainMenu.Run();
  }
}
