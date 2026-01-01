namespace AAI.Application.Common.Interfaces;

/// <summary>
/// Service for password hashing and verification using Argon2id.
/// </summary>
public interface IPasswordHashingService
{
    /// <summary>
    /// Hashes a password using Argon2id.
    /// </summary>
    /// <param name="password">Plain text password</param>
    /// <param name="salt">Output parameter with generated salt (Base64)</param>
    /// <returns>Password hash (Base64)</returns>
    string HashPassword(string password, out string salt);

    /// <summary>
    /// Verifies a password against a hash.
    /// </summary>
    /// <param name="password">Plain text password to verify</param>
    /// <param name="hash">Stored hash (Base64)</param>
    /// <param name="salt">Stored salt (Base64)</param>
    /// <returns>True if password matches, false otherwise</returns>
    bool VerifyPassword(string password, string hash, string salt);
}
