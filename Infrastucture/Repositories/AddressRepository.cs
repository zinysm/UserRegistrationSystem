using Infrastructure.Persistence;
using UserRegistrations.Domain.Entities;
using UserRegistrations.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UserRegistrations.Infrastucture.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Address?> GetByIdAsync(Guid id)
    {
        return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Address address)
    {
        await _context.Addresses.AddAsync(address);
    }

    public async Task UpdateAsync(Address address)
    {
        _context.Addresses.Update(address);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Address address)
    {
        _context.Addresses.Remove(address);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
