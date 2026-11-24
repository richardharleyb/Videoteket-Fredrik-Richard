using System.Data;
using Dapper;
using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Infrastructure.Repositories;

public class PhysicalCopyRepository : BaseRepository, IPhysicalCopyRepository
{
  public bool IsCopyRented(string barcode)
  {
    return false;
  }

  public void AddPhysicalCopy(PhysicalCopy physicalCopy)
  {
    using IDbConnection connection = CreateConnection();

    connection.Execute(
      """
      INSERT INTO PhysicalCopy(Barcode, IsActive, MovieId)
      VALUES(@Barcode, @IsActive, @MovieId)
      """,
      physicalCopy
    );
  }

  //Kollar vad som finns på "Lager", det som inte är uthyrt
  public List<AvailableCopyDto> FetchAvailableCopies()
  {
    using IDbConnection connection = CreateConnection();

    List<AvailableCopyDto> availableCopies =
    [
      .. connection.Query<AvailableCopyDto>(
        """
        SELECT PhysicalCopy.Id, PhysicalCopy.Barcode,
        Movie.Title
        FROM PhysicalCopy
        JOIN Movie ON Movie.Id = PhysicalCopy.MovieId
        WHERE PhysicalCopy.IsActive
          AND PhysicalCopy.Id NOT IN (
            SELECT PhysicalCopyId
            FROM RentalItem
            WHERE ReturnedAt IS NULL
          );
        """
      ),
    ];

    return availableCopies;
  }

  public void SetInactive(string barcode)
  {
    using IDbConnection connection = CreateConnection();

    int rowsUpdated = connection.Execute(
      "UPDATE PhysicalCopy SET IsActive = false WHERE Barcode = @Barcode;",
      new { Barcode = barcode }
    );

    if (rowsUpdated == 0)
    {
      throw new InvalidOperationException($"Barcode {barcode} not found");
    }
  }
}
