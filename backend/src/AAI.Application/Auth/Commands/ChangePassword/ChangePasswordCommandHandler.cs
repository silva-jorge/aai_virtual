using MediatR;
using AAI.Application.Common.Interfaces;
using AAI.Domain.Interfaces;

namespace AAI.Application.Auth.Commands.ChangePassword;

/// <summary>
/// Handler for changing user password
/// </summary>
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResponse>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IPasswordHashingService _passwordHasher;

    public ChangePasswordCommandHandler(
        IUserProfileRepository userProfileRepository,
        IPasswordHashingService passwordHasher)
    {
        _userProfileRepository = userProfileRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<ChangePasswordResponse> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserId);

        // Get user by ID
        var user = await _userProfileRepository.GetByIdAsync(userId, cancellationToken);
        if (user == null)
        {
            return new ChangePasswordResponse
            {
                Success = false,
                Message = "User not found"
            };
        }

        // Verify current password
        if (!_passwordHasher.VerifyPassword(request.CurrentPassword, user.PasswordHash, user.PasswordSalt))
        {
            return new ChangePasswordResponse
            {
                Success = false,
                Message = "Current password is incorrect"
            };
        }

        // Hash and set new password
        var hashedPassword = _passwordHasher.HashPassword(request.NewPassword, out var salt);
        user.PasswordHash = hashedPassword;
        user.PasswordSalt = salt;

        // Save changes
        await _userProfileRepository.UpdateAsync(user, cancellationToken);

        return new ChangePasswordResponse
        {
            Success = true,
            Message = "Password changed successfully"
        };
    }
}
