namespace Videoteket.src.Domain;

internal class PhysicalCopy
{
  public int MovieId { get; private set; }
  public string Barcode { get; private set; }
  public bool IsActive { get; private set; }
}
