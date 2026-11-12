namespace Videoteket.src.Domain;

internal class PhysicalCopy
{
    public int MovieId { get; private set; }
    public decimal Barcode { get; private set; }//Ska de va decimal?
    public bool IsActive { get; private set; }
}