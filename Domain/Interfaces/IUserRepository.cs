using UserRegistrations.Domain.Entities;

namespace UserRegistrations.Domain.Interfaces;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task<List<User>> GetAllAsync();
    Task DeleteAsync(User user);
    Task SaveChangesAsync();
}
