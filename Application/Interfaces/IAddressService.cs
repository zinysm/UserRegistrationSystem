namespace UserRegistrations.Application.Interfaces;

public interface IAddressService
{
    Task UpdateCityAsync(Guid userId, string newCity);
    Task UpdateStreetAsync(Guid userId, string newStreet);
    Task UpdateHouseNumberAsync(Guid userId, string newHouseNumber);
    Task UpdateApartmentNumberAsync(Guid userId, string? newApartmentNumber);
}
