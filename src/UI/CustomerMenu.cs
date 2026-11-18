using Videoteket.src.Domain;
using Videoteket.src.Services;

namespace Videoteket.src.UI;

internal class CustomerMenu : IMenuOption
{
  private readonly CustomerService _customerService = new CustomerService();

  public void Run()
  {
    bool running = true;

    while (running)
    {
      Console.Clear();
      Console.WriteLine("CUSTOMER MENU:");
      Console.WriteLine("1: Search Customers");
      Console.WriteLine("2: Add Customer");
      Console.WriteLine("3: Edit Customer");
      Console.WriteLine("4: Delete Customer");
      Console.WriteLine("5: Back to Main Menu");

      Console.Write("Choice: ");
      if (!int.TryParse(Console.ReadLine(), out int input))
      {
        input = 0;
      }

      switch (input)
      {
        case 1:
          SearchCustomers();
          break;
        case 2:
          AddNewCustomer();
          break;
        case 3:
          break;
        case 4:
          DeleteCustomer();
          break;
        case 5:
          running = false;
          break;
        default:
          Console.WriteLine("Invalid choice. Press any key to try again...");
          Console.ReadKey();
          break;
      }
    }
  }

  private void AddNewCustomer()
  {
    string name = ConsoleInputReader.ReadNonEmptyText("Name: ");
    string phoneNumber = ConsoleInputReader.ReadNonEmptyText("Phonenumber: ");
    string? email = ConsoleInputReader.ReadOptionalText("Email: ");
    string address = ConsoleInputReader.ReadNonEmptyText("Address: ");

    bool saved = _customerService.AddCustomer(name, phoneNumber, email, address);

    if (saved)
    {
      Console.WriteLine("New customer added.");
    }
    else
    {
      Console.WriteLine("Error:\nNo customer was saved.\nPlease try again later.");
    }

    ConsoleInputReader.WaitForAnyKey();
  }

  private void SearchCustomers()
  {
    string name = ConsoleInputReader.ReadNonEmptyText("Name: ");
    List<Customer>? customersFound = _customerService.SearchCustomers(name);
    if (customersFound == null)
    {
      Console.WriteLine("Error: Unable to connect with database");
    }
    else if (customersFound.Count == 0)
    {
      Console.WriteLine("No customer found");
    }
    else
    {
      for (int i = 0; i < customersFound.Count; i++)
      {
        Console.WriteLine(
          $"{customersFound[i].Name} - {customersFound[i].PhoneNumber} - {customersFound[i].Email} - {customersFound[i].Address}"
        );
      }
    }
    ConsoleInputReader.WaitForAnyKey();
  }

  private void DeleteCustomer()
  {
    string phoneNumber = ConsoleInputReader.ReadNonEmptyText("Phonenumber: ");
    bool removed = _customerService.DeleteCustomer(phoneNumber);

    if (removed)
    {
      Console.WriteLine("Customer removed");
    }
    else
    {
      Console.WriteLine("Error:\nNo customer was removed.\nPlease try again later.");
    }

    ConsoleInputReader.WaitForAnyKey();
  }

  public override string ToString()
  {
    return "Customer Menu";
  }
}
