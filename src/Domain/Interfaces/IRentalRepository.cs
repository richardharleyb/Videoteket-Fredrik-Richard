namespace Videoteket.src.Domain.Interfaces;

internal interface IRentalRepository
{
  void CreateRental(int customerId, List<string> physicalCopyIds);
  List<Receipt> FetchReceipt(int rentalId);
}
