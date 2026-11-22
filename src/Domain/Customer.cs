namespace Videoteket.src.Domain;

internal class Customer : Entity
{
  public string Name { get; private set; }
  public string PhoneNumber { get; private set; }
  public string? Email { get; private set; }
  public string Address { get; private set; }

  public Customer(string name, string phoneNumber, string? email, string address)
  {
    if (string.IsNullOrWhiteSpace(name))
      throw new ArgumentException("Customer must contain a name");

    if (string.IsNullOrWhiteSpace(phoneNumber))
      throw new ArgumentException("Customer must contain a phonenumber");

    if (string.IsNullOrWhiteSpace(address))
      throw new ArgumentException("Customer must contain an address");

    Name = name;
    PhoneNumber = phoneNumber;
    Email = email;
    Address = address;
  }

  private Customer() { } // for Dapper only: So we can select id
}
