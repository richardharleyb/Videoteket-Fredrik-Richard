namespace Videoteket.src.UI;

internal class MovieMenu : IMenuOption
{
  public void Run()
  {
    bool running = true;

    while (running)
    {
      Console.Clear();
      Console.WriteLine("MOVIE MENU:");
      Console.WriteLine("1: View Movies");
      Console.WriteLine("2: Add Movie");
      Console.WriteLine("3: Edit Movie");
      Console.WriteLine("4: Delete Movie");
      Console.WriteLine("5: Back to Main Menu");

      int choice = ConsoleInputReader.ReadIntInRange("Choice: ", 1, 5);

      switch (choice)
      {
        case 1:
          break;
        case 2:
          break;
        case 3:
          break;
        case 4:
          break;
        case 5:
          running = false;
          break;
      }
    }
  }

  public override string ToString()
  {
    return "Movie Menu";
  }
}
