using Videoteket.src.Domain.Interfaces;
using Videoteket.src.Infrastructure.Repositories;

namespace Videoteket.src.Services;

internal class RentalService
{
  private readonly IRentalRepository _rentalRepository = new RentalRepository();

  public bool CreateRental(int customerId, List<int> physicalCopyIds)
  {
    if (physicalCopyIds.Count == 0)
    {
      return false;
    }
    try
    {
      _rentalRepository.CreateRental(customerId, physicalCopyIds);
      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }
}
