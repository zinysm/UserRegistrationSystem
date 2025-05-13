using Infrastructure.Persistence;
using UserRegistrations.Domain.Entities;
using UserRegistrations.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UserRegistrations.Infrastucture.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            return await _context.Persons
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
        }

        public async Task UpdateAsync(Person person)
        {
            _context.Persons.Update(person);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Person person)
        {
            _context.Persons.Remove(person);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
