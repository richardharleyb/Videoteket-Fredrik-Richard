using System.Data;
using Dapper;
using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Infrastructure.Repositories;

internal class RentalRepository : BaseRepository, IRentalRepository
{
  public int CreateRental(int customerId, List<string> barcodes)
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
        int rowsInserted = connection.Execute(
          """
          INSERT INTO RentalItem ( RentalId, PhysicalCopyId)
          SELECT @RentalId, Id
          FROM PhysicalCopy
          WHERE Barcode=@Barcode;
          """,
          new { RentalId = rentalId, Barcode = barcode },
          transaction: transaction
        );
        if (rowsInserted == 0)
        {
          throw new InvalidOperationException($"Barcode {barcode} not found"); //rollback if barcode inserts wrong
        }
      }

      transaction.Commit();
      return rentalId;
    }
    catch
    {
      transaction.Rollback(); //Undo if anything fails
      throw; //Let serviceLayer know the transaction failed
    }
  }

  public void ReturnMovies(string barcode)
  {
    using IDbConnection connection = CreateConnection();

    int rowsUpdated = connection.Execute(
      """
      UPDATE RentalItem
      SET ReturnedAt = NOW(),
      LateFee = IF(DATEDIFF (NOW(), ExpectedReturn) > 0,
      DATEDIFF(NOW(), ExpectedReturn) * Price, 0)
      WHERE PhysicalCopyId = (
        SELECT Id FROM PhysicalCopy WHERE Barcode = @Barcode)
      AND ReturnedAt IS NULL
      """,
      new { Barcode = barcode }
    );

    if (rowsUpdated == 0)
    {
      throw new InvalidOperationException(
        $"Unable to update moviestatus. Check {barcode} and try again"
      );
    }
  }

  public List<Receipt> FetchReceipt(int rentalId)
  {
    using IDbConnection connection = CreateConnection();

    List<Receipt> receiptRows =
    [
      .. connection.Query<Receipt>(
        """
        SELECT Rental.Id AS RentalId, Customer.Name AS CustomerName,
        Rental.RentedAt, Movie.Title, RentalItem.Price, RentalItem.LateFee
        FROM Rental
        JOIN Customer ON Customer.Id = Rental.CustomerId
        JOIN RentalItem ON RentalItem.RentalId = Rental.Id
        JOIN PhysicalCopy ON PhysicalCopy.Id = RentalItem.PhysicalCopyId
        JOIN Movie ON Movie.Id = PhysicalCopy.MovieId
        WHERE Rental.Id = @rentalId;
        """,
        new { rentalId }
      ),
    ];

    return receiptRows;
  }
}
