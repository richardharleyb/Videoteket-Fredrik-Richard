using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;
using Videoteket.src.Infrastructure.Repositories;

namespace Videoteket.src.Services;

internal class PhysicalCopyService
{
  private readonly IPhysicalCopyRepository _physicalCopyRepository = new PhysicalCopyRepository();

  public bool AddPhysicalCopy(PhysicalCopy newCopy)
  {
    try
    {
      PhysicalCopy physicalCopy = new(newCopy.Barcode, newCopy.IsActive, newCopy.MovieId);

      _physicalCopyRepository.AddPhysicalCopy(physicalCopy);

      return true;
    }
    catch (Exception)
    {
      return false;
    }
  }
}
