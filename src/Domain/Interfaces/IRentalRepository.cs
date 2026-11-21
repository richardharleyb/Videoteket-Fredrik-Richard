namespace Videoteket.src.Domain.Interfaces;

internal interface IRentalRepository
{
  void CreateRental(int customerId, List<int> physicalCopyIds);
}
