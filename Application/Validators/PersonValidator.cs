using FluentValidation;
using UserRegistrations.Application.DTOs;
using UserRegistrations.Application.Validators;

namespace UserRegistrations.Application.Validators;

public class PersonValidator : AbstractValidator<PersonCreateForm>
{
    public PersonValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");
        this.ApplyPersonRules(x => x.FirstName);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");
        this.ApplyPersonRules(x => x.LastName);

        RuleFor(x => x.PersonalCode)
            .NotEmpty().WithMessage("Personal code is required.")
            .Matches("^[0-9]{11}$").WithMessage("Personal code must be exactly 11 digits.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.");
        this.ApplyPhoneRule(x => x.PhoneNumber);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.");
        this.ApplyEmailRule(x => x.Email);

        RuleFor(x => x.ProfileImage)
            .NotNull().WithMessage("Profile image is required.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100);

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(100);

        RuleFor(x => x.HouseNumber)
            .NotEmpty().WithMessage("House number is required.")
            .MaximumLength(10);

        RuleFor(x => x.ApartmentNumber)
            .MaximumLength(10)
            .When(x => !string.IsNullOrWhiteSpace(x.ApartmentNumber));
    }
}
