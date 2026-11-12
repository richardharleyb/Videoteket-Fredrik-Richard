namespace Videoteket.src.Domain;

internal class Customer : Entity
{
  public string Name { get; private set; }
  public string PhoneNumber { get; private set; }
  public string Email { get; private set; }
  public string Adress { get; private set; }
}
