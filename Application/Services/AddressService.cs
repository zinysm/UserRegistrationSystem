using UserRegistrations.Application.Interfaces;
using UserRegistrations.Domain.Interfaces;

namespace UserRegistrations.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;

        public AddressService(IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }

        public async Task UpdateCityAsync(Guid userId, string newCity)
        {
            var address = await GetUserAddressAsync(userId);
            address.City = newCity;
            await _addressRepository.SaveChangesAsync();
        }

        public async Task UpdateStreetAsync(Guid userId, string newStreet)
        {
            var address = await GetUserAddressAsync(userId);
            address.Street = newStreet;
            await _addressRepository.SaveChangesAsync();
        }

        public async Task UpdateHouseNumberAsync(Guid userId, string newHouseNumber)
        {
            var address = await GetUserAddressAsync(userId);
            address.HouseNumber = newHouseNumber;
            await _addressRepository.SaveChangesAsync();
        }

        public async Task UpdateApartmentNumberAsync(Guid userId, string? newApartmentNumber)
        {
            var address = await GetUserAddressAsync(userId);
            address.ApartmentNumber = newApartmentNumber;
            await _addressRepository.SaveChangesAsync();
        }

        private async Task<Domain.Entities.Address> GetUserAddressAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId)
                ?? throw new Exception("User not found.");
            if (user.Person == null || user.Person.Address == null)
                throw new Exception("Address not found.");
            return user.Person.Address;
        }
    }
}
