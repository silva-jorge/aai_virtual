using MediatR;

namespace AAI.Application.Auth.Commands.RefreshToken;

/// <summary>
/// Command to refresh an expired JWT token
/// </summary>
public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
{
    public string RefreshToken { get; set; } = string.Empty;
}

/// <summary>
/// Response from RefreshTokenCommand
/// </summary>
public class RefreshTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; } = "Bearer";
}
