namespace Videoteket.src.Domain;

internal class RentalItem
{
  public int RentalId { get; private set; }
  public DateTime ExpectedReturn { get; private set; }
  public DateTime ReturnedAt { get; private set; }

  public decimal LateFee { get; private set; }
}
