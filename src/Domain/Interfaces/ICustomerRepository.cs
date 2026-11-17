namespace Videoteket.src.Domain.Interfaces;

internal interface ICustomerRepository
{
  void AddCustomer(Customer customer);
  void DeleteCustomer(string phoneNumber);
}
