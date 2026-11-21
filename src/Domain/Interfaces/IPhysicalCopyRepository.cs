namespace Videoteket.src.Domain.Interfaces;

public interface IPhysicalCopyRepository
{
  public bool IsCopyRented(string barcode);
  public void AddPhysicalCopy(PhysicalCopy physicalCopy);

  //Kollar vad som finns på "Lager", det som inte är uthyrt
  public List<AvailableCopyDto> FetchAvailableCopies();
}
