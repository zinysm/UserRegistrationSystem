using FluentValidation;
using UserRegistrations.Application.DTOs;
namespace UserRegistrations.Application.Validators;
public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
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
