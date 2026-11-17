namespace Videoteket.src.Domain.Interfaces;

internal interface IPhysicalCopyRepository
{
  public void AddPhysicalCopy(PhysicalCopy physicalCopy);

  //Kollar vad som finns på "Lager", det som inte är uthyrt
  public List<AvailableCopyDto> FetchAvailableCopies();
}
