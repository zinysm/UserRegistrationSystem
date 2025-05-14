using UserRegistrations.Application.DTOs;
using UserRegistrations.Application.Interfaces;
using Application.Interfaces;
using UserRegistrations.Domain.Entities;
using UserRegistrations.Domain.Enums;
using UserRegistrations.Domain.Interfaces;

namespace UserRegistrations.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        IJwtService jwtService,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(dto.Username);
        if (existingUser != null)
            throw new Exception("User already exists");

        var salt = _passwordHasher.GenerateSalt();
        var hash = _passwordHasher.HashPassword(dto.Password, salt);

        var user = new User
        {
            Username = dto.Username,
            Salt = salt,
            PasswordHash = hash,
            Role = RoleType.User
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }
    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("User not found");

        await _userRepository.DeleteAsync(user);
        await _userRepository.SaveChangesAsync();
    }
    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null)
            throw new Exception("User not found");

        if (!_passwordHasher.VerifyPassword(dto.Password, user.Salt, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        return _jwtService.GenerateToken(user);
    }
}
