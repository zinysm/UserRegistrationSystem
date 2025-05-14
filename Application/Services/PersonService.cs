using UserRegistrations.Application.DTOs;
using UserRegistrations.Application.Interfaces;
using UserRegistrations.Domain.Entities;
using UserRegistrations.Domain.Interfaces;

namespace UserRegistrations.Application.Services;

public class PersonService : IPersonService
{
    private readonly IUserRepository _userRepository;

    public PersonService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task UploadPersonInfoAsync(Guid userId, PersonCreateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new Exception("User not found.");

        if (user.Person != null)
            throw new Exception("Profile already uploaded.");

        var person = new Person
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PersonalCode = dto.PersonalCode,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            ProfileImage = dto.ProfileImage,
            Address = new Address
            {
                City = dto.Address.City,
                Street = dto.Address.Street,
                HouseNumber = dto.Address.HouseNumber,
                ApartmentNumber = dto.Address.ApartmentNumber
            }
        };

        user.Person = person;

        await _userRepository.SaveChangesAsync();
    }

    public async Task<PersonCreateDto> GetPersonByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new Exception("User not found.");

        var person = user.Person ?? throw new Exception("Profile not found.");

        return new PersonCreateDto
        {
            FirstName = person.FirstName,
            LastName = person.LastName,
            PersonalCode = person.PersonalCode,
            PhoneNumber = person.PhoneNumber,
            Email = person.Email,
            ProfileImage = person.ProfileImage,
            Address = new AddressDto
            {
                City = person.Address?.City ?? "",
                Street = person.Address?.Street ?? "",
                HouseNumber = person.Address?.HouseNumber ?? "",
                ApartmentNumber = person.Address?.ApartmentNumber
            }
        };
    }

    public async Task UpdateProfileImageAsync(Guid userId, byte[] image)
    {
        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new Exception("User not found.");

        if (user.Person == null)
            throw new Exception("Profile not found.");

        user.Person.ProfileImage = image;

        await _userRepository.SaveChangesAsync();
    }

    public async Task UpdatePartialInfoAsync(Guid userId, PersonUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new Exception("User not found.");
        if (user.Person == null)
            throw new Exception("Person info not found.");

        var person = user.Person;

        if (!string.IsNullOrWhiteSpace(dto.FirstName))
            person.FirstName = dto.FirstName;

        if (!string.IsNullOrWhiteSpace(dto.LastName))
            person.LastName = dto.LastName;

        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
            person.PhoneNumber = dto.PhoneNumber;

        if (!string.IsNullOrWhiteSpace(dto.Email))
            person.Email = dto.Email;

        var address = person.Address;
        if (address != null)
        {
            if (!string.IsNullOrWhiteSpace(dto.Address.City))
                address.City = dto.Address.City;

            if (!string.IsNullOrWhiteSpace(dto.Address.Street))
                address.Street = dto.Address.Street;

            if (!string.IsNullOrWhiteSpace(dto.Address.HouseNumber))
                address.HouseNumber = dto.Address.HouseNumber;

            if (!string.IsNullOrWhiteSpace(dto.Address.ApartmentNumber))
                address.ApartmentNumber = dto.Address.ApartmentNumber;
        }

        await _userRepository.SaveChangesAsync();
    }
}
