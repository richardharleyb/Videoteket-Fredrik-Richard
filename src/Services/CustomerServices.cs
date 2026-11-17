using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;
using Videoteket.src.Infrastructure.Repositories;

namespace Videoteket.src.Services;

internal class CustomerServices
{
  private readonly ICustomerRepository _customerRepository = new CustomerRepository();

  public bool AddCustomer(string name, string phoneNumber, string? email, string address)
  {
    try
    {
      Customer customer = new Customer(name, phoneNumber, email, address);
      _customerRepository.AddCustomer(customer);
      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }

  public List<Customer>? SearchCustomers(string name)
  {
    try
    {
      List<Customer> customers = _customerRepository.SearchCustomers(name);
      return customers;
    }
    catch
    {
      return null;
    }
  }
}
