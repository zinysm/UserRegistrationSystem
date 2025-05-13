namespace UserRegistrations.Application.DTOs;

public class PersonCreateDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PersonalCode { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string? ApartmentNumber { get; set; }

    public IFormFile ProfileImage { get; set; } = null!;
}
