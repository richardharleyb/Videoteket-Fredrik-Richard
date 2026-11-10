namespace Videoteket.src.Domain;

internal class CustomerOrder
{
  public int CustomerId { get; private set; }
  public DateTime RentedAt { get; private set; }
  public DateTime RentedDue { get; private set; }
}
