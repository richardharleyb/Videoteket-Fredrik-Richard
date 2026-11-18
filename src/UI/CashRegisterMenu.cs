namespace Videoteket.src.UI;

internal class CashRegisterMenu : IMenuOption
{
  public void Run()
  {
    bool running = true;

    while (running)
    {
      Console.Clear();
      Console.WriteLine("CASH REGISTER MENU:");
      Console.WriteLine("1: New Rental (Create Order)");
      Console.WriteLine("2: Return Movies");
      Console.WriteLine("3: View Available Copies");
      Console.WriteLine("4: Manage Prices");
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
    return "Cash Register Menu";
  }
}
