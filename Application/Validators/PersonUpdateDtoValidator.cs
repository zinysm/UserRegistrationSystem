using FluentValidation;
using UserRegistrations.Application.DTOs;

namespace UserRegistrations.Application.Validators;

public class PersonUpdateDtoValidator : AbstractValidator<PersonUpdateDto>
{
    public PersonUpdateDtoValidator()
    {
        this.ApplyPersonRules(x => x.FirstName);
        this.ApplyPersonRules(x => x.LastName);
        this.ApplyEmailRule(x => x.Email);
        this.ApplyPhoneRule(x => x.PhoneNumber);

        RuleFor(x => x.Address).SetValidator(new AddressDtoValidator());
    }
}
