namespace Videoteket.src.Domain;

public class Movie(string title, DateOnly releaseDate, bool isNew) : Entity
{
  public string Title { get; private set; } = title;
  public DateOnly ReleaseDate { get; private set; } = releaseDate;
  public bool IsNew { get; private set; } = isNew;

  private Movie()
    : this("", default, false) { } // For Dapper, fills properties
}
