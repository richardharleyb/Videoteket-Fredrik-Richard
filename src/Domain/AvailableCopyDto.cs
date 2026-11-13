namespace Videoteket.src.Domain;

internal class AvailableCopyDto : Entity
{
  public string Barcode { get; set; } = string.Empty;
  public string Title { get; set; } = string.Empty;
}
