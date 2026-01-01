using AAI.Application.Common.Interfaces;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace AAI.Infrastructure.Security;

public class PasswordHashingService : IPasswordHashingService
{
    private const int SaltSize = 32; // 256 bits
    private const int HashSize = 32; // 256 bits
    private const int Iterations = 4;
    private const int MemorySize = 65536; // 64 MB
    private const int DegreeOfParallelism = 2;

    public string HashPassword(string password, out string salt)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password cannot be empty", nameof(password));
        }

        // Generate random salt
        var saltBytes = GenerateSalt();
        salt = Convert.ToBase64String(saltBytes);

        // Hash password with Argon2id
        var hash = HashPasswordWithSalt(password, saltBytes);

        return Convert.ToBase64String(hash);
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        if (string.IsNullOrWhiteSpace(password) || 
            string.IsNullOrWhiteSpace(hash) || 
            string.IsNullOrWhiteSpace(salt))
        {
            return false;
        }

        try
        {
            var saltBytes = Convert.FromBase64String(salt);
            var hashBytes = Convert.FromBase64String(hash);

            var testHash = HashPasswordWithSalt(password, saltBytes);

            return CryptographicOperations.FixedTimeEquals(hashBytes, testHash);
        }
        catch
        {
            return false;
        }
    }

    private static byte[] GenerateSalt()
    {
        var salt = new byte[SaltSize];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }

    private static byte[] HashPasswordWithSalt(string password, byte[] salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        using var argon2 = new Argon2id(passwordBytes)
        {
            Salt = salt,
            DegreeOfParallelism = DegreeOfParallelism,
            MemorySize = MemorySize,
            Iterations = Iterations
        };

        return argon2.GetBytes(HashSize);
    }
}
