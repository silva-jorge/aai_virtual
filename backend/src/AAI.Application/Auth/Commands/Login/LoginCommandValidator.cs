using FluentValidation;

namespace AAI.Application.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Pin)
            .NotEmpty().WithMessage("PIN é obrigatório")
            .Length(4, 6).WithMessage("PIN deve ter entre 4 e 6 dígitos")
            .Matches("^[0-9]+$").WithMessage("PIN deve conter apenas números");
    }
}
