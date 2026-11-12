using System.Data;
using Dapper;
using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Infrastructure.Repositories;

internal class PhysicalCopyRepository : BaseRepository, IPhysicalCopyRepository
{
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
}
