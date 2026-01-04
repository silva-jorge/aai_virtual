using MediatR;

namespace AAI.Application.Auth.Commands.ChangePassword;

/// <summary>
/// Command to change a user's password
/// </summary>
public class ChangePasswordCommand : IRequest<ChangePasswordResponse>
{
    public string UserId { get; set; } = string.Empty;
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

/// <summary>
/// Response from ChangePasswordCommand
/// </summary>
public class ChangePasswordResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
