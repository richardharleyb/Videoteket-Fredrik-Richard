using Videoteket.src.Domain.Interfaces;

namespace Videoteket.src.Services;

public class RentalService(IPhysicalCopyRepository physicalCopyRepository)
{
  private readonly IPhysicalCopyRepository _physicalCopyRepository = physicalCopyRepository;

  public void Rent(string barcode) { }
}
