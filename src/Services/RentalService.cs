using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;
using Videoteket.src.Infrastructure.Repositories;

namespace Videoteket.src.Services;

public class RentalService(IPhysicalCopyRepository physicalCopyRepository)
{
  private readonly IPhysicalCopyRepository _physicalCopyRepository = physicalCopyRepository;
  private readonly IRentalRepository _rentalRepository = new RentalRepository();

  public RentalService()
    : this(new PhysicalCopyRepository()) { }

  public int CreateRental(int customerId, List<string> barcodes)
  {
    if (barcodes.Count == 0)
    {
      throw new ArgumentException("The order has to have at least one item");
    }

    for (int i = 0; i < barcodes.Count; i++)
    {
      Rent(barcodes[i]);
    }

    return _rentalRepository.CreateRental(customerId, barcodes);
  }

  public decimal ReturnMovies(string barcode)
  {
    if (string.IsNullOrWhiteSpace(barcode))
    {
      throw new ArgumentException("Barcode cannot be empty");
    }
    return _rentalRepository.ReturnMovies(barcode);
  }

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

  public List<Receipt> FetchReceipt(int rentalId)
  {
    if (rentalId <= 0)
    {
      throw new ArgumentException("RentalId must be greater than 0");
    }
    return _rentalRepository.FetchReceipt(rentalId);
  }
}
