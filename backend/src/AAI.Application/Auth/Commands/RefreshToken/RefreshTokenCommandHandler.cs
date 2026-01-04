using MediatR;
using AAI.Application.Common.Interfaces;
using AAI.Domain.Interfaces;

namespace AAI.Application.Auth.Commands.RefreshToken;

/// <summary>
/// Handler for refreshing JWT tokens
/// </summary>
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IUserProfileRepository _userProfileRepository;

    public RefreshTokenCommandHandler(
        IJwtTokenService jwtTokenService,
        IUserProfileRepository userProfileRepository)
    {
        _jwtTokenService = jwtTokenService;
        _userProfileRepository = userProfileRepository;
    }

    public async Task<RefreshTokenResponse> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Validate and extract user ID from refresh token
            var isValid = _jwtTokenService.ValidateRefreshToken(request.RefreshToken, out var userId);

            if (!isValid || userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            // Get user to verify still exists and active
            var user = await _userProfileRepository.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            // Generate new tokens
            var accessToken = _jwtTokenService.GenerateAccessToken(user.Id);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            return new RefreshTokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = 3600, // 1 hour
                TokenType = "Bearer"
            };
        }
        catch (Exception ex)
        {
            throw new UnauthorizedAccessException("Token refresh failed", ex);
        }
    }
}
