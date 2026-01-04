using MediatR;

namespace AAI.Application.Auth.Commands.SetupPassword;

/// <summary>
/// Command to set up initial password for a user (first-time setup)
/// </summary>
public class SetupPasswordCommand : IRequest<SetupPasswordResponse>
{
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

/// <summary>
/// Response from SetupPasswordCommand
/// </summary>
public class SetupPasswordResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
