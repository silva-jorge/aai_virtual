using MediatR;
using AAI.Application.Common.Interfaces;
using AAI.Domain.Interfaces;

namespace AAI.Application.Auth.Commands.SetupPassword;

/// <summary>
/// Handler for setting up initial password for a user
/// </summary>
public class SetupPasswordCommandHandler : IRequestHandler<SetupPasswordCommand, SetupPasswordResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashingService _passwordHasher;

    public SetupPasswordCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHashingService passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<SetupPasswordResponse> Handle(
        SetupPasswordCommand request,
        CancellationToken cancellationToken)
    {
        // Get user by ID
        var user = await _unitOfWork.UserProfiles.GetByIdAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return new SetupPasswordResponse
            {
                Success = false,
                Message = "User not found"
            };
        }

        // Check if password already exists
        if (!string.IsNullOrEmpty(user.PasswordHash))
        {
            return new SetupPasswordResponse
            {
                Success = false,
                Message = "User already has a password set"
            };
        }

        // Hash and set password
        var hashedPassword = _passwordHasher.HashPassword(request.Password);
        user.SetPassword(hashedPassword);

        // Save changes
        _unitOfWork.UserProfiles.Update(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new SetupPasswordResponse
        {
            Success = true,
            Message = "Password set successfully"
        };
    }
}
