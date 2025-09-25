

namespace Veterinary.Models;

public class Customer
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required string Address { get; set; }
  public required string Email { get; set; }
  public required string Phone { get; set; }


  public List<Pet> Pets { get; set; } = new();


  public override string ToString()
  {
    return $"[CustomerID: {Id}] {Name}, Email: {Email}, Phone: {Phone}, Address: {Address}, Pets: {Pets.Count}";
  }
}

