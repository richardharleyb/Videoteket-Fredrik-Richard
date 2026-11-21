using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.Tests;

public class StubPhysicalCopyRepository(List<AvailableCopyDto> availableCopies)
  : IPhysicalCopyRepository
{
  private readonly List<AvailableCopyDto> _availableCopies = availableCopies;

  public void AddPhysicalCopy(PhysicalCopy physicalCopy) { }

  //Kollar vad som finns på "Lager", det som inte är uthyrt
  public List<AvailableCopyDto> FetchAvailableCopies()
  {
    return _availableCopies;
  }
}
