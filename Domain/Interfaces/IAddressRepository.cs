using UserRegistrations.Domain.Entities;

namespace UserRegistrations.Domain.Interfaces;

public interface IAddressRepository
{
    Task<Address?> GetByIdAsync(Guid id);
    Task AddAsync(Address address);
    Task UpdateAsync(Address address);
    Task DeleteAsync(Address address);
    Task SaveChangesAsync();
}
