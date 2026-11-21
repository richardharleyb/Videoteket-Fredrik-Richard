namespace Videoteket.src.Domain;

public class PhysicalCopy(string barcode, bool isActive, int movieId) : Entity
{
  public string Barcode { get; private set; } =
    string.IsNullOrWhiteSpace(barcode)
      ? throw new ArgumentException("Barcode cannot be empty")
      : barcode;
  public bool IsActive { get; private set; } = isActive;
  public int MovieId { get; private set; } =
    movieId <= 0 ? throw new ArgumentException("MovieId must be greater than zero") : movieId;

  public void Rent() { }
}
