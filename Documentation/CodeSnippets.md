```csharp
 //Visa lediga exemplar i konsolen
PhysicalCopyRepository physicalCopyRepository = new();
List<AvailableCopyDto> physicalCopies = physicalCopyRepository.FetchAvailableCopyDto();

for (int i = 0; i < physicalCopies.Count; i++)
{
  Console.WriteLine($"Title: {physicalCopies[i].Title}\nBarcode: {physicalCopies[i].Barcode}");
}
```
