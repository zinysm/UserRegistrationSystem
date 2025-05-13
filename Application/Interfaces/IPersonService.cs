using UserRegistrations.Application.DTOs;

namespace UserRegistrations.Application.Interfaces;

public interface IPersonService
{
    Task UploadPersonInfoAsync(Guid userId, PersonCreateDto dto);
    Task<PersonCreateDto> GetPersonByIdAsync(Guid userId);
    Task UpdateProfileImageAsync(Guid userId, byte[] image);
    Task UpdatePartialInfoAsync(Guid userId, PersonUpdateDto dto);
}
