namespace Videoteket.src.Domain;

public class Receipt
{
  public int RentalId { get; set; }
  public string CustomerName { get; set; } = string.Empty;
  public DateTime RentedAt { get; set; }
  public string Title { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public decimal? LateFee { get; set; }
}
