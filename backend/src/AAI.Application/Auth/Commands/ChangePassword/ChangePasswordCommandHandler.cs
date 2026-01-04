using MediatR;
using AAI.Application.Common.Interfaces;
using AAI.Domain.Interfaces;

namespace AAI.Application.Auth.Commands.ChangePassword;

/// <summary>
/// Handler for changing user password
/// </summary>
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashingService _passwordHasher;

    public ChangePasswordCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHashingService passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<ChangePasswordResponse> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken)
    {
        // Get user by ID
        var user = await _unitOfWork.UserProfiles.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return new ChangePasswordResponse
            {
                Success = false,
                Message = "User not found"
            };
        }

        // Verify current password
        if (!_passwordHasher.VerifyPassword(request.CurrentPassword, user.PasswordHash))
        {
            return new ChangePasswordResponse
            {
                Success = false,
                Message = "Current password is incorrect"
            };
        }

        // Hash and set new password
        var hashedPassword = _passwordHasher.HashPassword(request.NewPassword);
        user.SetPassword(hashedPassword);

        // Save changes
        _unitOfWork.UserProfiles.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ChangePasswordResponse
        {
            Success = true,
            Message = "Password changed successfully"
        };
    }
}
