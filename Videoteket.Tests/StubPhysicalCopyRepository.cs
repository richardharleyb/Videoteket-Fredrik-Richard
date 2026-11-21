using Videoteket.src.Domain;
using Videoteket.src.Domain.Interfaces;

namespace Videoteket.Tests;

public class StubPhysicalCopyRepository(
  List<AvailableCopyDto> availableCopies,
  bool isRented = true
) : IPhysicalCopyRepository
{
  private readonly bool _isRented = isRented;
  private readonly List<AvailableCopyDto> _availableCopies = availableCopies;

  public bool IsCopyRented(string barcode)
  {
    return _isRented;
  }

  public void AddPhysicalCopy(PhysicalCopy physicalCopy) { }

  //Kollar vad som finns på "Lager", det som inte är uthyrt
  public List<AvailableCopyDto> FetchAvailableCopies()
  {
    return _availableCopies;
  }
}
