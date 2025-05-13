using System.Text;
using UserRegistrations.Application.DTOs;
using System.Security.Cryptography;
using UserRegistrations.Application.Interfaces;
using UserRegistrations.Domain.Entities;
using UserRegistrations.Domain.Enums;
using UserRegistrations.Domain.Interfaces;

namespace UserRegistrations.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public UserService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(dto.Username);
        if (existingUser != null)
            throw new Exception("User already exists");

        var salt = Guid.NewGuid().ToString();
        var passwordHash = HashPassword(dto.Password, salt);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Salt = salt,
            PasswordHash = passwordHash,
            Role = RoleType.User
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null)
            throw new Exception("Invalid username or password");

        var hashed = HashPassword(dto.Password, user.Salt);
        if (hashed != user.PasswordHash)
            throw new Exception("Invalid username or password");

        return _jwtService.GenerateToken(user);
    }

    private string HashPassword(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var combined = Encoding.UTF8.GetBytes(password + salt);
        var hash = sha256.ComputeHash(combined);
        return Convert.ToBase64String(hash);
    }
    public async Task DeleteUserAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("User not found.");

        await _userRepository.DeleteAsync(user);
        await _userRepository.SaveChangesAsync();
    }
}
