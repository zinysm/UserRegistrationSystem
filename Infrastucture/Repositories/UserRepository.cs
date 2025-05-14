using Infrastructure.Persistence;
using UserRegistrations.Domain.Entities;
using UserRegistrations.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UserRegistrations.Infrastucture.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Person)
            .ThenInclude(p => p.Address)
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Person)
            .ThenInclude(p => p.Address)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Person)
            .ThenInclude(p => p.Address)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task DeleteAsync(User user)
    {

        _context.Users.Remove(user);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
