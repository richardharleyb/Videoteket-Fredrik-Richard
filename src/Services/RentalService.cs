using Videoteket.src.Domain.Interfaces;
using Videoteket.src.Infrastructure.Repositories;

namespace Videoteket.src.Services;

public class RentalService(IPhysicalCopyRepository physicalCopyRepository)
{
  private readonly IPhysicalCopyRepository _physicalCopyRepository = physicalCopyRepository;
  private readonly IRentalRepository _rentalRepository = new RentalRepository();

  public RentalService()
    : this(new PhysicalCopyRepository()) { }

  public void CreateRental(int customerId, List<string> barcodes)
  {
    if (barcodes.Count == 0)
    {
      throw new ArgumentException("The order has to have at least one item");
    }

    for (int i = 0; i < barcodes.Count; i++)
    {
      Rent(barcodes[i]);
    }

    _rentalRepository.CreateRental(customerId, barcodes);
  }
  public void ReturnMovies()

  public void SetInactive(string barcode)
  {
    if (_physicalCopyRepository.IsCopyRented(barcode))
      throw new InvalidOperationException($"{barcode} cannot be set to inactive when being rented");
  }

  private void Rent(string barcode)
  {
    if (_physicalCopyRepository.IsCopyRented(barcode))
      throw new InvalidOperationException($"{barcode} is already rented");
  }
}
