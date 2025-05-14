using UserRegistrations.Domain.Entities;
using UserRegistrations.Domain.Enums;
using UserRegistrations.Domain.Interfaces;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace UserRegistrations.Infrastructure.Persistence;

public class DataSeeder
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;

    public DataSeeder(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task SeedAdminAsync()
    {
        var username = _configuration["AdminSeed:Username"];
        var password = _configuration["AdminSeed:Password"];
        var idText = _configuration["AdminSeed:Id"];

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            throw new Exception("Admin seed configuration is missing.");

        var existing = await _userRepository.GetByUsernameAsync(username);
        if (existing != null) return;

        Guid.TryParse(idText, out var adminId);
        var salt = _passwordHasher.GenerateSalt();
        var hash = _passwordHasher.HashPassword(password, salt);

        var user = new User
        {
            Id = adminId == Guid.Empty ? Guid.NewGuid() : adminId,
            Username = username,
            Salt = salt,
            PasswordHash = hash,
            Role = RoleType.Admin
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
    }
}
