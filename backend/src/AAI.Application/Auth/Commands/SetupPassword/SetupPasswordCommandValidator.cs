using FluentValidation;

namespace AAI.Application.Auth.Commands.SetupPassword;

/// <summary>
/// Validator for SetupPasswordCommand
/// </summary>
public class SetupPasswordCommandValidator : AbstractValidator<SetupPasswordCommand>
{
    public SetupPasswordCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)")
            .WithMessage("Password must contain uppercase, lowercase and digits");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Password confirmation is required")
            .Equal(x => x.Password).WithMessage("Passwords do not match");
    }
}
