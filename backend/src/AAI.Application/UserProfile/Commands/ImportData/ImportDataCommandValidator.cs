using FluentValidation;

namespace AAI.Application.UserProfile.Commands.ImportData;

/// <summary>
/// Validator for ImportDataCommand
/// </summary>
public class ImportDataCommandValidator : AbstractValidator<ImportDataCommand>
{
    public ImportDataCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required")
            .Must(BeValidGuid)
            .WithMessage("User ID must be a valid GUID");

        RuleFor(x => x)
            .Must(HaveAtLeastOneDataType)
            .WithMessage("Import must contain at least UserProfile, Portfolios, Positions, or Recommendations");

        When(x => x.UserProfile != null, () =>
        {
            RuleFor(x => x.UserProfile!.RiskProfile)
                .NotEmpty()
                .WithMessage("Risk profile is required when importing user profile");

            RuleFor(x => x.UserProfile!.VolatilityTolerance)
                .InclusiveBetween(0, 100)
                .WithMessage("Volatility tolerance must be between 0 and 100");

            RuleFor(x => x.UserProfile!.TimeHorizonMonths)
                .GreaterThan(0)
                .WithMessage("Time horizon must be greater than 0");

            RuleFor(x => x.UserProfile!.RebalanceThresholdPercent)
                .InclusiveBetween(1, 50)
                .WithMessage("Rebalance threshold must be between 1 and 50");
        });

        When(x => x.Portfolios.Any(), () =>
        {
            RuleForEach(x => x.Portfolios).ChildRules(portfolio =>
            {
                portfolio.RuleFor(p => p.Id)
                    .NotEmpty()
                    .WithMessage("Portfolio ID is required")
                    .Must(BeValidGuid)
                    .WithMessage("Portfolio ID must be a valid GUID");

                portfolio.RuleFor(p => p.Name)
                    .NotEmpty()
                    .WithMessage("Portfolio name is required")
                    .MaximumLength(100)
                    .WithMessage("Portfolio name must not exceed 100 characters");

                portfolio.RuleFor(p => p.Currency)
                    .NotEmpty()
                    .WithMessage("Portfolio currency is required")
                    .Must(BeValidCurrency)
                    .WithMessage("Currency must be a valid ISO 4217 code (BRL, USD, EUR, etc.)");
            });
        });

        When(x => x.Positions.Any(), () =>
        {
            RuleForEach(x => x.Positions).ChildRules(position =>
            {
                position.RuleFor(p => p.Id)
                    .NotEmpty()
                    .WithMessage("Position ID is required")
                    .Must(BeValidGuid)
                    .WithMessage("Position ID must be a valid GUID");

                position.RuleFor(p => p.PortfolioId)
                    .NotEmpty()
                    .WithMessage("Portfolio ID is required for position")
                    .Must(BeValidGuid)
                    .WithMessage("Portfolio ID must be a valid GUID");

                position.RuleFor(p => p.AssetId)
                    .NotEmpty()
                    .WithMessage("Asset ID is required for position")
                    .Must(BeValidGuid)
                    .WithMessage("Asset ID must be a valid GUID");

                position.RuleFor(p => p.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Position quantity must be greater than 0");

                position.RuleFor(p => p.AverageCost)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Average cost must be greater than or equal to 0");
            });
        });

        When(x => x.Recommendations.Any(), () =>
        {
            RuleForEach(x => x.Recommendations).ChildRules(recommendation =>
            {
                recommendation.RuleFor(r => r.Id)
                    .NotEmpty()
                    .WithMessage("Recommendation ID is required")
                    .Must(BeValidGuid)
                    .WithMessage("Recommendation ID must be a valid GUID");

                recommendation.RuleFor(r => r.PortfolioId)
                    .NotEmpty()
                    .WithMessage("Portfolio ID is required for recommendation")
                    .Must(BeValidGuid)
                    .WithMessage("Portfolio ID must be a valid GUID");

                recommendation.RuleFor(r => r.ActionType)
                    .NotEmpty()
                    .WithMessage("Action type is required for recommendation");

                recommendation.RuleFor(r => r.Justification)
                    .NotEmpty()
                    .WithMessage("Justification is required for recommendation");
            });
        });
    }

    private bool BeValidGuid(string? value)
    {
        return !string.IsNullOrEmpty(value) && Guid.TryParse(value, out _);
    }

    private bool HaveAtLeastOneDataType(ImportDataCommand command)
    {
        return command.UserProfile != null ||
               command.Portfolios.Any() ||
               command.Positions.Any() ||
               command.Recommendations.Any();
    }

    private bool BeValidCurrency(string currency)
    {
        // Common currency codes - expand as needed
        var validCurrencies = new[] { "BRL", "USD", "EUR", "GBP", "JPY", "CHF", "CAD", "AUD" };
        return validCurrencies.Contains(currency.ToUpperInvariant());
    }
}
