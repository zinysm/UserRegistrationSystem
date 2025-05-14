using UserRegistrations.Application.DTOs;
using UserRegistrations.Application.Interfaces;
using Application.Interfaces;
using UserRegistrations.Domain.Entities;
using UserRegistrations.Domain.Enums;
using UserRegistrations.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace UserRegistrations.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IJwtService _jwtService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;
    private readonly Guid _protectedAdminId;

    public UserService(
        IUserRepository userRepository,
        IPersonRepository personRepository,
        IAddressRepository addressRepository,
        IJwtService jwtService,
        IPasswordHasher passwordHasher,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _personRepository = personRepository;
        _addressRepository = addressRepository;
        _jwtService = jwtService;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
        var adminIdString = configuration["AdminSeed:Id"];
        if (string.IsNullOrWhiteSpace(adminIdString))
            throw new Exception("AdminSeed:Id is not configured.");
        _protectedAdminId = Guid.Parse(adminIdString);
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

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null)
            throw new Exception("User not found");

        if (!_passwordHasher.VerifyPassword(dto.Password, user.Salt, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid credentials");

        return _jwtService.GenerateToken(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("User not found");

        if (user.Id == _protectedAdminId)
            throw new Exception("The main administrator cannot be deleted.");

        if (user.Person?.Address != null)
            await _addressRepository.DeleteAsync(user.Person.Address);

        if (user.Person != null)
            await _personRepository.DeleteAsync(user.Person);

        await _userRepository.DeleteAsync(user);
        await _userRepository.SaveChangesAsync();
    }

}
