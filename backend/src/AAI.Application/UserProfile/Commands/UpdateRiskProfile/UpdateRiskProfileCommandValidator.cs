using FluentValidation;

namespace AAI.Application.UserProfile.Commands.UpdateRiskProfile;

public class UpdateRiskProfileCommandValidator : AbstractValidator<UpdateRiskProfileCommand>
{
    public UpdateRiskProfileCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");

        RuleFor(x => x.RiskProfile)
            .NotEmpty()
            .WithMessage("RiskProfile is required")
            .Must(BeValidRiskProfile)
            .WithMessage("RiskProfile must be 'conservador', 'moderado', or 'agressivo'");

        RuleFor(x => x.VolatilityTolerance)
            .InclusiveBetween(0, 100)
            .WithMessage("VolatilityTolerance must be between 0 and 100");

        RuleFor(x => x.TimeHorizonMonths)
            .GreaterThan(0)
            .WithMessage("TimeHorizonMonths must be greater than 0");

        RuleFor(x => x.InvestmentGoal)
            .MaximumLength(500)
            .When(x => !string.IsNullOrEmpty(x.InvestmentGoal))
            .WithMessage("InvestmentGoal cannot exceed 500 characters");
    }

    private bool BeValidRiskProfile(string riskProfile)
    {
        var validProfiles = new[] { "conservador", "moderado", "agressivo" };
        return validProfiles.Contains(riskProfile.ToLower());
    }
}
