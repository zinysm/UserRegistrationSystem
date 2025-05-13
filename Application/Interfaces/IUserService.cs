using UserRegistrations.Application.DTOs;

namespace UserRegistrations.Application.Interfaces;

public interface IUserService
{
    Task RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
    Task DeleteUserAsync(Guid userId);
}
