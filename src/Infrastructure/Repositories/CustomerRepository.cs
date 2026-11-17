using System.Data;
using Dapper;
using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Infrastructure.Repositories;

internal class CustomerRepository : BaseRepository, ICustomerRepository
{
  public void AddCustomer(Customer customer)
  {
    using IDbConnection connection = CreateConnection();

    connection.Execute(
      """
      INSERT INTO Customer (Name, PhoneNumber, Email, Address)
      VALUES (@Name, @PhoneNumber, @Email, @Address);
      """,
      customer
    );
  }

  public void DeleteCustomer(string phoneNumber)
  {
    using IDbConnection connection = CreateConnection();

    connection.Execute(
      """
      DELETE FROM Customer
      WHERE PhoneNumber= @Phonenumber;
      """,
      new { PhoneNumber = phoneNumber }
    );
  }

  public List<Customer> SearchCustomers(string name)
  {
    using IDbConnection connection = CreateConnection();

    List<Customer> customers =
    [
      .. connection.Query<Customer>(
        "SELECT Name, Phonenumber, Email, Address FROM Customer WHERE Customer.Name LIKE @name;",
        new { name = $"{name}%" }
      ),
    ];

    return customers;
  }
}
