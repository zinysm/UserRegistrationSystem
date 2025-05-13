using FluentValidation;
using System.Linq.Expressions;

namespace UserRegistrations.Application.Validators;
public static class PersonValidationRules
{
    public static void ApplyPersonRules<T>(this AbstractValidator<T> validator, Expression<Func<T, string?>> selector)
    {
        validator.RuleFor(selector)
            .Matches("^[A-ZĄČĘĖĮŠŲŪŽa-ząčęėįšųūž\\s-]+$")
            .WithMessage("Only letters are allowed.")
            .When(x => !string.IsNullOrWhiteSpace(selector.Compile()(x)));
    }

    public static void ApplyEmailRule<T>(this AbstractValidator<T> validator, Expression<Func<T, string?>> selector)
    {
        validator.RuleFor(selector)
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .When(x => !string.IsNullOrWhiteSpace(selector.Compile()(x)));
    }

    public static void ApplyPhoneRule<T>(this AbstractValidator<T> validator, Expression<Func<T, string?>> selector)
    {
        validator.RuleFor(selector)
            .Matches(@"^\+?\d{8,15}$")
            .WithMessage("Phone number must be valid.")
            .When(x => !string.IsNullOrWhiteSpace(selector.Compile()(x)));
    }
}

