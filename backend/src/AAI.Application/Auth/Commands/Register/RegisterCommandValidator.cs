using FluentValidation;

namespace AAI.Application.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Pin)
            .NotEmpty().WithMessage("PIN é obrigatório")
            .Length(4, 6).WithMessage("PIN deve ter entre 4 e 6 dígitos")
            .Matches("^[0-9]+$").WithMessage("PIN deve conter apenas números");

        RuleFor(x => x.RiskProfile)
            .NotEmpty().WithMessage("Perfil de risco é obrigatório")
            .Must(x => new[] { "conservador", "moderado", "agressivo", "personalizado" }.Contains(x))
            .WithMessage("Perfil de risco inválido");

        RuleFor(x => x.InvestmentGoal)
            .MaximumLength(500).WithMessage("Objetivo de investimento deve ter no máximo 500 caracteres");

        RuleFor(x => x.VolatilityTolerance)
            .InclusiveBetween(0, 100).WithMessage("Tolerância a volatilidade deve estar entre 0 e 100");

        RuleFor(x => x.TimeHorizonMonths)
            .InclusiveBetween(1, 600).WithMessage("Horizonte temporal deve estar entre 1 e 600 meses");

        RuleFor(x => x.RebalanceThresholdPercent)
            .InclusiveBetween(1, 50).WithMessage("Threshold de rebalanceamento deve estar entre 1 e 50");
    }
}
