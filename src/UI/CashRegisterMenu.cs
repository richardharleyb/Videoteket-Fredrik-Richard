
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
          ReturnMovies();
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
    List<AvailableCopyDto>? availableCopies = _physicalCopyService.FetchAvailableCopies();
    if (availableCopies == null)
    {
      Console.WriteLine("Error: Unable to connect");
      ConsoleInputReader.WaitForAnyKey();
      return;
    }
    if (availableCopies.Count == 0)
    {
      Console.WriteLine("There´s no movies left in stock.");
      ConsoleInputReader.WaitForAnyKey();
      return;
    }
    List<string> barcodes = [];
    while (true)
    {
      for (int i = 0; i < availableCopies.Count; i++)
      {
        Console.WriteLine($"{i + 1}: {availableCopies[i].Title}");
      }
      int pick = ConsoleInputReader.ReadIntInRange(
        "Choose movie (0 = done): ",
        0,
        availableCopies.Count
      );

      if (pick == 0)
      {
        break; //Leave the loop and continue order
      }
      barcodes.Add(availableCopies[pick - 1].Barcode); // add choise
      availableCopies.RemoveAt(pick - 1); // remove from list so we cant add twice
    }
    try
    {
      int rentalId = _rentalService.CreateRental(customer.Id, barcodes);
      Console.WriteLine($"New order created. Order number {rentalId}");
      PrintReceipt(rentalId);
    }
    catch (Exception exception)
    {
      Console.WriteLine($"Error: {exception.Message}");
    }
    ConsoleInputReader.WaitForAnyKey();
  }

  private void ReturnMovies()
  {
    string barcode = ConsoleInputReader.ReadNonEmptyText("Barcode: ");
    try
    {
      _rentalService.ReturnMovies(barcode);

      Console.WriteLine("Movie returned.");
    }
    catch (Exception exception)
    {
      Console.WriteLine($"Error: {exception.Message}");
    }
    ConsoleInputReader.WaitForAnyKey();
  }

  public override string ToString()
  {
    return "Cash Register Menu";
  }

  private void PrintReceipt(int rentalId)
  {
    List<Receipt> rows = _rentalService.FetchReceipt(rentalId);

    if (rows.Count == 0)
    {
      Console.WriteLine("No receipt found");
      return;


    }
    Receipt first = rows[0];
    Console.WriteLine("Videohallen");
    Console.WriteLine("----------------------");
    Console.WriteLine("Receipt");
    Console.WriteLine($"Order: {first.RentalId}");
    Console.WriteLine($"Customer: {first.CustomerName}");
    Console.WriteLine($"Date: {first.RentedAt:yyyy-MM-dd HH:mm}");
    Console.WriteLine("----------------------");

    foreach (Receipt row in rows)
    {
      Console.WriteLine($"{row.Title} {row.Price:C}");
    }

    decimal total = rows.Sum(row => row.Price);
    Console.WriteLine($"Total: {total:C}");
    Console.WriteLine("----------------------");
    Console.WriteLine("Välkommen till onödig text");
  }
}
