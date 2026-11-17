namespace Videoteket.src.Domain.Interfaces;

internal interface ICustomerRepository
{
  void AddCustomer(Customer customer);
  public List<Customer> SearchCustomers(string name);
}
