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
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(
        IJwtTokenService jwtTokenService,
        IUnitOfWork unitOfWork)
    {
        _jwtTokenService = jwtTokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<RefreshTokenResponse> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Validate and extract user ID from refresh token
            var (userId, isValid) = _jwtTokenService.ValidateRefreshToken(request.RefreshToken);

            if (!isValid || string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            // Get user to verify still exists and active
            var user = await _unitOfWork.UserProfiles.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            // Generate new tokens
            var accessToken = _jwtTokenService.GenerateAccessToken(user.Id, user.Email);
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
