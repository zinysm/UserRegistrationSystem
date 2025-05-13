using UserRegistrations.Domain.Entities;

namespace UserRegistrations.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}
