using FluentValidation;
using System.Text.Json;

namespace AAI.Application.UserProfile.Commands.UpdateThresholds;

public class UpdateThresholdsCommandValidator : AbstractValidator<UpdateThresholdsCommand>
{
    public UpdateThresholdsCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId is required");

        RuleFor(x => x.RebalanceThresholdPercent)
            .InclusiveBetween(0, 100)
            .WithMessage("RebalanceThresholdPercent must be between 0 and 100");

        RuleFor(x => x.TargetAllocationJson)
            .NotEmpty()
            .WithMessage("TargetAllocationJson is required")
            .Must(BeValidJson)
            .WithMessage("TargetAllocationJson must be valid JSON");
    }

    private bool BeValidJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return false;

        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
