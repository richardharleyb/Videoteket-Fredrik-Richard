using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Services;

public class RentalService(IPhysicalCopyRepository physicalCopyRepository)
{
  private readonly IPhysicalCopyRepository _physicalCopyRepository = physicalCopyRepository;

  public void Rent(string barcode)
  {
    if (_physicalCopyRepository.IsCopyRented(barcode))
      throw new InvalidOperationException($"{barcode} is already rented");
  }

  public void SetInactive(string barcode) { }
}
