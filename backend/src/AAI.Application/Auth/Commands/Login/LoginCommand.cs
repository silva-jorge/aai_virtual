using MediatR;

namespace AAI.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<LoginResponse>
{
    public string Pin { get; init; } = string.Empty;
}
