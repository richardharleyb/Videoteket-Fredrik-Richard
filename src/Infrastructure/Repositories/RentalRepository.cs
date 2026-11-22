using System.Data;
using Dapper;
using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Infrastructure.Repositories;

internal class RentalRepository : BaseRepository, IRentalRepository
{
  public void CreateRental(int customerId, List<int> physicalCopyIds)
  {
    using IDbConnection connection = CreateConnection();
    connection.Open();
    using IDbTransaction transaction = connection.BeginTransaction();

    try
    {
      connection.Execute(
        "INSERT INTO Rental (CustomerId, RentedAt) VALUES (@CustomerId, NOW());",
        new { CustomerId = customerId },
        transaction: transaction
      );
      int rentalId = connection.QuerySingle<int>(
        "SELECT LAST_INSERT_ID();",
        transaction: transaction
      );
      foreach (int physicalCopyId in physicalCopyIds)
      {
        connection.Execute(
          "INSERT INTO RentalItem ( RentalId, PhysicalCopyId) VALUES (@RentalId, @PhysicalCopyId)",
          new { RentalId = rentalId, PhysicalCopyId = physicalCopyId },
          transaction: transaction
        );
      }

      transaction.Commit();
    }
    catch
    {
      transaction.Rollback(); //Undo if anything fails
      throw; //Let serviceLayer know the transaction failed
    }
  }
}
