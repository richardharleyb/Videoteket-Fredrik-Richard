//TODO: lägg in AddNewPhysicalCopy i MovieMenu när den finns

using Videoteket.src.Domain;
using Videoteket.src.Services;

namespace Videoteket.src.UI;

internal class NewPhysicalCopy : IMenuOption
{
  private readonly PhysicalCopyService _physicalCopyService = new();

  public void Run() { }
}
