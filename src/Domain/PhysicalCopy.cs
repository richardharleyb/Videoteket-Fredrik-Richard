namespace Videoteket.src.Domain;

internal class PhysicalCopy
{
    public int MovieId { get; set; }
    public decimal Barcode { get; set; }//Ska de va decimal?
    bool IsActive;
}