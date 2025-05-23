﻿namespace UserRegistrations.Application.DTOs;
public class PersonUpdateDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public AddressDto Address { get; set; } = null!;
}
