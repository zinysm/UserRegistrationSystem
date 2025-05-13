using FluentValidation;
using UserRegistrations.Application.DTOs;

namespace UserRegistrations.Application.Validators;

public class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
        RuleFor(x => x.Street).NotEmpty().WithMessage("Street is required.");
        RuleFor(x => x.HouseNumber).NotEmpty().WithMessage("House number is required.");
    }
}
