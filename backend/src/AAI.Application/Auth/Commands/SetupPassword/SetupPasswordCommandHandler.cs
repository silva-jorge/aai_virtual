using MediatR;
using AAI.Application.Common.Interfaces;
using AAI.Domain.Interfaces;

namespace AAI.Application.Auth.Commands.SetupPassword;

/// <summary>
/// Handler for setting up initial password for a user
/// </summary>
public class SetupPasswordCommandHandler : IRequestHandler<SetupPasswordCommand, SetupPasswordResponse>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IPasswordHashingService _passwordHasher;

    public SetupPasswordCommandHandler(
        IUserProfileRepository userProfileRepository,
        IPasswordHashingService passwordHasher)
    {
        _userProfileRepository = userProfileRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<SetupPasswordResponse> Handle(
        SetupPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(request.UserId);

        // Get user by ID
        var user = await _userProfileRepository.GetByIdAsync(userId, cancellationToken);
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
        var hashedPassword = _passwordHasher.HashPassword(request.Password, out var salt);
        user.PasswordHash = hashedPassword;
        user.PasswordSalt = salt;

        // Save changes
        await _userProfileRepository.UpdateAsync(user, cancellationToken);

        return new SetupPasswordResponse
        {
            Success = true,
            Message = "Password set successfully"
        };
    }
}
