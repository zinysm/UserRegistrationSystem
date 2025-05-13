using FluentValidation;
using UserRegistrations.Application.DTOs;
using UserRegistrations.Application.Validators;

namespace UserRegistrations.Application.Validators;

public class PersonUpdateDtoValidator : AbstractValidator<PersonUpdateDto>
{
    public PersonUpdateDtoValidator()
    {
        this.ApplyPersonRules(x => x.FirstName);
        this.ApplyPersonRules(x => x.LastName);
        this.ApplyEmailRule(x => x.Email);
        this.ApplyPhoneRule(x => x.PhoneNumber);

        RuleFor(x => x.City)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.City));

        RuleFor(x => x.Street)
            .MaximumLength(100)
            .When(x => !string.IsNullOrWhiteSpace(x.Street));

        RuleFor(x => x.HouseNumber)
            .MaximumLength(10)
            .When(x => !string.IsNullOrWhiteSpace(x.HouseNumber));

        RuleFor(x => x.ApartmentNumber)
            .MaximumLength(10)
            .When(x => !string.IsNullOrWhiteSpace(x.ApartmentNumber));
    }
}
