namespace Videoteket.src.Domain;

internal class Movie : Entity
{
  public string Title { get; private set; }
  public DateOnly ReleaseDate { get; private set; }
  public bool IsNew { get; private set; } = false;
}
