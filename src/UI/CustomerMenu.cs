using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;
using Videoteket.src.Infrastructure.Repositories;

namespace Videoteket.src.UI;

internal class CustomerMenu : IMenuOption
{
  private readonly ICustomerRepository _customerRepository = new CustomerRepository();

  public void Run()
  {
    bool running = true;

    while (running)
    {
      Console.Clear();
      Console.WriteLine("CUSTOMER MENU:");
      Console.WriteLine("1: View Customers");
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
          break;
        case 2:
          AddNewCustomer();
          break;
        case 3:
          break;
        case 4:
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
    Customer customer = new Customer(name, phoneNumber, email, address);
    try
    {
      _customerRepository.AddCustomer(customer);
      Console.WriteLine("New customer added.");
    }
    catch (Exception)
    {
      Console.WriteLine("Error:\nNo customer was saved. \nPlease try again later.");
    }

    ConsoleInputReader.WaitForAnyKey();
  }

  public override string ToString()
  {
    return "Customer Menu";
  }
}
