namespace UserRegistrations.Application.DTOs;

public class PersonCreateDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string PersonalCode { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public AddressDto Address { get; set; } = null!;
    public byte[] ProfileImage { get; set; } = null!;


}
