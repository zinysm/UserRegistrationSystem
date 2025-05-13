using FluentValidation;
using UserRegistrations.Application.DTOs;

namespace UserRegistrations.Application.Validators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches("[A-Z]").WithMessage("Must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Must contain at least one special character.");
    }
}
