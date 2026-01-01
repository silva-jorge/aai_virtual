namespace AAI.Application.Common.Interfaces;

/// <summary>
/// Service for JWT token generation and validation.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates a JWT token for a user.
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="expirationMinutes">Token expiration in minutes (default: 60)</param>
    /// <returns>JWT token string</returns>
    string GenerateToken(Guid userId, int expirationMinutes = 60);

    /// <summary>
    /// Validates a JWT token and extracts the user ID.
    /// </summary>
    /// <param name="token">JWT token string</param>
    /// <returns>User ID if valid, null otherwise</returns>
    Guid? ValidateToken(string token);
}
