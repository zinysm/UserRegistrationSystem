using UserRegistrations.Application.DTOs;
using UserRegistrations.Application.Interfaces;
using UserRegistrations.Domain.Entities;
using UserRegistrations.Domain.Interfaces;

namespace UserRegistrations.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IAddressRepository _addressRepository;

        public PersonService(
            IUserRepository userRepository,
            IPersonRepository personRepository,
            IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
            _addressRepository = addressRepository;
        }

        public async Task UploadPersonInfoAsync(Guid userId, PersonDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found.");

            if (user.PersonId != null)
                throw new Exception("Person information already exists.");

            var address = new Address
            {
                Id = Guid.NewGuid(),
                City = dto.Address.City,
                Street = dto.Address.Street,
                HouseNumber = dto.Address.HouseNumber,
                ApartmentNumber = dto.Address.ApartmentNumber
            };
            await _addressRepository.AddAsync(address);

            var person = new Person
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PersonalCode = dto.PersonalCode,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                ProfileImage = dto.ProfileImage,
                AddressId = address.Id,
                Address = address
            };
            await _personRepository.AddAsync(person);

            user.PersonId = person.Id;
            user.Person = person;

            await _userRepository.SaveChangesAsync();
        }

        public async Task<PersonDto> GetPersonByIdAsync(Guid personId)
        {
            var person = await _personRepository.GetByIdAsync(personId)
                ?? throw new Exception("Person not found.");

            return new PersonDto
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                PersonalCode = person.PersonalCode,
                PhoneNumber = person.PhoneNumber,
                Email = person.Email,
                ProfileImage = person.ProfileImage,
                Address = new AddressDto
                {
                    City = person.Address.City,
                    Street = person.Address.Street,
                    HouseNumber = person.Address.HouseNumber,
                    ApartmentNumber = person.Address.ApartmentNumber
                }
            };
        }

        public async Task UpdateFirstNameAsync(Guid userId, string newFirstName)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found.");
            if (user.Person == null)
                throw new Exception("Person info not found.");

            user.Person.FirstName = newFirstName;
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateLastNameAsync(Guid userId, string newLastName)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found.");
            if (user.Person == null)
                throw new Exception("Person info not found.");

            user.Person.LastName = newLastName;
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdatePersonalCodeAsync(Guid userId, string newPersonalCode)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found.");
            if (user.Person == null)
                throw new Exception("Person info not found.");

            user.Person.PersonalCode = newPersonalCode;
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdatePhoneNumberAsync(Guid userId, string newPhoneNumber)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found.");
            if (user.Person == null)
                throw new Exception("Person info not found.");

            user.Person.PhoneNumber = newPhoneNumber;
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateEmailAsync(Guid userId, string newEmail)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found.");
            if (user.Person == null)
                throw new Exception("Person info not found.");

            user.Person.Email = newEmail;
            await _userRepository.SaveChangesAsync();
        }

        public async Task UpdateProfileImageAsync(Guid userId, byte[] newImage)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found.");
            if (user.Person == null)
                throw new Exception("Person info not found.");

            user.Person.ProfileImage = newImage;
            await _userRepository.SaveChangesAsync();
        }
    }
}
