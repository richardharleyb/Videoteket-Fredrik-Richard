using Videoteket.src.Domain;
using Videoteket.src.Services;

namespace Videoteket.src.UI;

internal class MovieMenu : IMenuOption
{
  private readonly PhysicalCopyService _physicalCopyService = new();

  public void Run()
  {
    bool running = true;

    while (running)
    {
      Console.Clear();
      Console.WriteLine("MOVIE MENU:");
      Console.WriteLine("1: View Movies");
      Console.WriteLine("2: Add Physical Copy");
      Console.WriteLine("3: Edit Movie");
      Console.WriteLine("4: Delete Movie");
      Console.WriteLine("5: Back to Main Menu");

      int choice = ConsoleInputReader.ReadIntInRange("Choice: ", 1, 5);

      switch (choice)
      {
        case 1:
          break;
        case 2:
          AddNewPhysicalCopy();
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

  private void AddNewPhysicalCopy()
  {
    string barcode = ConsoleInputReader.ReadNonEmptyText("Barcode ");
    bool isActive = ConsoleInputReader.ReadYesOrNo("IsActive ");
    //TODO: hämta totalt antal filmer och lägg in istället för hårdkodat 1,10
    int movieId = ConsoleInputReader.ReadIntInRange("MovieId, ", 1, 10);

    PhysicalCopy physicalCopy = new(barcode, isActive, movieId);

    bool saved = _physicalCopyService.AddPhysicalCopy(physicalCopy);

    if (saved)
    {
      Console.WriteLine("New physical copy added");
    }
    else
    {
      Console.WriteLine("Error:\nNo physical copy was saved.\nPlease try again later.");
    }
    ConsoleInputReader.WaitForAnyKey();
  }

  public override string ToString()
  {
    return "Movie Menu";
  }
}
