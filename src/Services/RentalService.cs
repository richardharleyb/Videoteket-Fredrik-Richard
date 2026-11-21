using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;
using Videoteket.src.Infrastructure.Repositories;

namespace Videoteket.src.Services;

public class RentalService(IPhysicalCopyRepository physicalCopyRepository)
{
  private readonly IPhysicalCopyRepository _physicalCopyRepository = physicalCopyRepository;
  private readonly IRentalRepository _rentalRepository = new RentalRepository();

  public RentalService() : this(new PhysicalCopyRepository()) { }

  public void Rent(string barcode)
  {
    if (_physicalCopyRepository.IsCopyRented(barcode))
      throw new InvalidOperationException($"{barcode} is already rented");
  }

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