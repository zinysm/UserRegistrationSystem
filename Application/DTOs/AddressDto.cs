namespace UserRegistrations.Application.DTOs;

public class AddressDto
{
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string? ApartmentNumber { get; set; }
}
