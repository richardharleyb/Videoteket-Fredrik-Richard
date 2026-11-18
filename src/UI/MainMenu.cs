namespace Videoteket.src.UI;

internal class MainMenu : IMenuOption
{
  private readonly List<IMenuOption> _subMenus =
  [
    new CashRegisterMenu(),
    new MovieMenu(),
    new CustomerMenu(),
  ];

  public void Run()
  {
    bool running = true;

    while (running)
    {
      Console.Clear();
      Console.WriteLine("MAIN MENU:");

      for (int i = 0; i < _subMenus.Count; i++)
      {
        Console.WriteLine($"{i + 1}: {_subMenus[i]}");
      }

      int exitChoice = _subMenus.Count + 1;
      Console.WriteLine($"{exitChoice}: Exit");

      int choice = ConsoleInputReader.ReadIntInRange("Choice: ", 1, exitChoice);

      if (choice == exitChoice)
      {
        running = false;
      }
      else
      {
        _subMenus[choice - 1].Run();
      }
    }
  }
}
