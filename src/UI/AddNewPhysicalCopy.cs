//TODO: lägg in AddNewPhysicalCopy i MovieMenu när den finns

using Videoteket.src.Domain;
using Videoteket.src.Services;

namespace Videoteket.src.UI;

internal class NewPhysicalCopy : IMenuOption
{
  private readonly PhysicalCopyService _physicalCopyService = new();

  public void Run() { }

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
}
