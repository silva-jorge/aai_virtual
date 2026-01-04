using FluentValidation;

namespace AAI.Application.UserProfile.Commands.DeleteAllData;

/// <summary>
/// Validator for DeleteAllDataCommand
/// </summary>
public class DeleteAllDataCommandValidator : AbstractValidator<DeleteAllDataCommand>
{
    public DeleteAllDataCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required")
            .Must(BeValidGuid)
            .WithMessage("User ID must be a valid GUID");

        RuleFor(x => x.ConfirmDeletion)
            .Equal(true)
            .WithMessage("Deletion must be confirmed by setting ConfirmDeletion to true. This operation is irreversible.");
    }

    private bool BeValidGuid(string? value)
    {
        return !string.IsNullOrEmpty(value) && Guid.TryParse(value, out _);
    }
}
