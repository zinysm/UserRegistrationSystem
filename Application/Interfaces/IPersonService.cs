using UserRegistrations.Application.DTOs;

namespace UserRegistrations.Application.Interfaces;

public interface IPersonService
{
    Task UploadPersonInfoAsync(Guid userId, PersonDto dto);
    Task<PersonDto> GetPersonByIdAsync(Guid personId);
    Task UpdateFirstNameAsync(Guid userId, string newFirstName);
    Task UpdateLastNameAsync(Guid userId, string newLastName);
    Task UpdatePersonalCodeAsync(Guid userId, string newPersonalCode);
    Task UpdatePhoneNumberAsync(Guid userId, string newPhoneNumber);
    Task UpdateEmailAsync(Guid userId, string newEmail);
    Task UpdateProfileImageAsync(Guid userId, byte[] newImage);
}
