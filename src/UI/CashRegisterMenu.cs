using Videoteket.src.Domain;
using Videoteket.src.Services;

namespace Videoteket.src.UI;

internal class CashRegisterMenu : IMenuOption
{
  private readonly RentalService _rentalService = new RentalService(); // ReadOnly field that can be used and not changed
  private readonly CustomerService _customerService = new CustomerService();
  private readonly PhysicalCopyService _physicalCopyService = new PhysicalCopyService();

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
          CreateNewRental();
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

  private void CreateNewRental()
  {
    string name = ConsoleInputReader.ReadNonEmptyText("Name: ");
    List<Customer>? customersFound = _customerService.SearchCustomers(name);
    if (customersFound == null)
    {
      Console.WriteLine("Error: Unable to connect");
      ConsoleInputReader.WaitForAnyKey();
      return; //Leave the method and create no new order
    }
    if (customersFound.Count == 0)
    {
      Console.WriteLine("Customer not found");
      ConsoleInputReader.WaitForAnyKey();
      return; // leave and add customer or start over
    }

    for (int i = 0; i < customersFound.Count; i++)
    {
      // +1 to not list customers as 0
      Console.WriteLine($"{i + 1}: {customersFound[i].Name} - {customersFound[i].PhoneNumber}");
    }
    int choice = ConsoleInputReader.ReadIntInRange("Choose customer: ", 1, customersFound.Count);
    //-1 so we get correct index
    Customer customer = customersFound[choice - 1];
  }

  public override string ToString()
  {
    return "Cash Register Menu";
  }
}
