namespace AAI.Application.Auth.Commands.Register;

public record RegisterResponse
{
    public Guid UserId { get; init; }
    public string Token { get; init; } = string.Empty;
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
}
