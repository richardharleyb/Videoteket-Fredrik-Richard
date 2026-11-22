using System.Data;
using Dapper;
using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Infrastructure.Repositories;

internal class RentalRepository : BaseRepository, IRentalRepository
{
  public void CreateRental(int customerId, List<string> barcodes)
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
      foreach (string barcode in barcodes)
      {
        connection.Execute(
          """
          INSERT INTO RentalItem ( RentalId, PhysicalCopyId)
          SELECT @RentalId, Id
          FROM PhysicalCopy
          WHERE Barcode=@Barcode;
          """,
          new { RentalId = rentalId, Barcode = barcode },
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
