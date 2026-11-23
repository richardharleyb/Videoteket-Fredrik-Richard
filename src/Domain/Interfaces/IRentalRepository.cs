namespace Videoteket.src.Domain.Interfaces;

internal interface IRentalRepository
{
  int CreateRental(int customerId, List<string> barcodes);
  decimal ReturnMovies(string barcode);
  List<Receipt> FetchReceipt(int rentalId);
}
