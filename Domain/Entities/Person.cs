namespace UserRegistrations.Domain.Entities;

public class Person
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PersonalCode { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] ProfileImage { get; set; } = null!;

    public Guid AddressId { get; set; }
    public Address Address { get; set; } = null!;
}
