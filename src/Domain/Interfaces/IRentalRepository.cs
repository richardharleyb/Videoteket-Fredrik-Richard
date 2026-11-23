namespace Videoteket.src.Domain.Interfaces;

internal interface IRentalRepository
{
  void CreateRental(int customerId, List<string> barcodes);
  void ReturnMovies(string barcode);
  List<Receipt> FetchReceipt(int rentalId);
}
