using FluentValidation;
using UserRegistrations.Application.DTOs;

namespace UserRegistrations.Application.Validators;
public class PersonValidator : AbstractValidator<PersonDto>
{
    public PersonValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");

        RuleFor(x => x.PersonalCode)
            .NotEmpty().WithMessage("Personal code is required.")
            .Matches("^[0-9]{11}$").WithMessage("Personal code must be exactly 11 digits.");

        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.");

        RuleFor(x => x.ProfileImage).NotNull().WithMessage("Profile image is required.");

        RuleFor(x => x.Address).SetValidator(new AddressValidator());
    }
}

