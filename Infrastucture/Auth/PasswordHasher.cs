using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;

namespace Infrastructure.Auth;
public class PasswordHasher : IPasswordHasher
{
    public string GenerateSalt()
    {
        var saltBytes = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    public string HashPassword(string password, string salt)
    {
        var combined = Encoding.UTF8.GetBytes(password + salt);
        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(combined);
            return Convert.ToBase64String(hash);
        }
    }

    public bool VerifyPassword(string password, string salt, string hashedPassword)
    {
        var hashToCheck = HashPassword(password, salt);
        return hashToCheck == hashedPassword;
    }
}
