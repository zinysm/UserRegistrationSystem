using Microsoft.EntityFrameworkCore;
using UserRegistrations.Domain.Entities;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User → Person (1:1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)
                .WithOne()
                .HasForeignKey<User>(u => u.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Person → Address (1:1)
            modelBuilder.Entity<Person>()
                .HasOne(p => p.Address)
                .WithOne()
                .HasForeignKey<Person>(p => p.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

