using UserRegistrations.Domain.Entities;

namespace UserRegistrations.Domain.Interfaces;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(Guid id);
    Task AddAsync(Person person);
    Task UpdateAsync(Person person);
    Task DeleteAsync(Person person);
    Task SaveChangesAsync();
}
