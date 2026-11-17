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
}
