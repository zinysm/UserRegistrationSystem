using UserRegistrations.Domain.Enums;

namespace UserRegistrations.Domain.Entities;
public class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Salt { get; set; } = null!;
    public RoleType Role { get; set; } = RoleType.User;

    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }
}
