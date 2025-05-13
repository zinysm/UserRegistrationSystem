using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserRegistrations.Application.DTOs;
using UserRegistrations.Application.Interfaces;
using UserRegistrations.Infrastucture.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IAddressService _addressService;
        private readonly ImageService _imageService;

        public ProfileController(IPersonService personService, IAddressService addressService, ImageService imageService)
        {
            _personService = personService;
            _addressService = addressService;
            _imageService = imageService;
        }

        private Guid GetUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.Parse(userIdString!);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPersonInfo([FromBody] PersonDto dto)
        {
            var userId = GetUserId();
            await _personService.UploadPersonInfoAsync(userId, dto);
            return Ok("Person info uploaded.");
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            var user = await _personService.GetPersonByIdAsync(userId);
            return Ok(user);
        }

        [HttpPut("firstname")]
        public async Task<IActionResult> UpdateFirstName([FromBody] string newFirstName)
        {
            var userId = GetUserId();
            await _personService.UpdateFirstNameAsync(userId, newFirstName);
            return Ok("First name updated.");
        }

        [HttpPut("lastname")]
        public async Task<IActionResult> UpdateLastName([FromBody] string newLastName)
        {
            var userId = GetUserId();
            await _personService.UpdateLastNameAsync(userId, newLastName);
            return Ok("Last name updated.");
        }

        [HttpPut("personalcode")]
        public async Task<IActionResult> UpdatePersonalCode([FromBody] string newCode)
        {
            var userId = GetUserId();
            await _personService.UpdatePersonalCodeAsync(userId, newCode);
            return Ok("Personal code updated.");
        }

        [HttpPut("phonenumber")]
        public async Task<IActionResult> UpdatePhoneNumber([FromBody] string newNumber)
        {
            var userId = GetUserId();
            await _personService.UpdatePhoneNumberAsync(userId, newNumber);
            return Ok("Phone number updated.");
        }

        [HttpPut("email")]
        public async Task<IActionResult> UpdateEmail([FromBody] string newEmail)
        {
            var userId = GetUserId();
            await _personService.UpdateEmailAsync(userId, newEmail);
            return Ok("Email updated.");
        }

        [HttpPut("profileimage")]
        public async Task<IActionResult> UpdateImage([FromForm] IFormFile file)
        {
            var userId = GetUserId();

            using var stream = file.OpenReadStream();
            var imageBytes = await _imageService.ProcessImageAsync(stream, file.FileName);
            await _personService.UpdateProfileImageAsync(userId, imageBytes);

            return Ok("Profile image updated.");
        }

        [HttpPut("city")]
        public async Task<IActionResult> UpdateCity([FromBody] string city)
        {
            var userId = GetUserId();
            await _addressService.UpdateCityAsync(userId, city);
            return Ok("City updated.");
        }

        [HttpPut("street")]
        public async Task<IActionResult> UpdateStreet([FromBody] string street)
        {
            var userId = GetUserId();
            await _addressService.UpdateStreetAsync(userId, street);
            return Ok("Street updated.");
        }

        [HttpPut("house-number")]
        public async Task<IActionResult> UpdateHouseNumber([FromBody] string number)
        {
            var userId = GetUserId();
            await _addressService.UpdateHouseNumberAsync(userId, number);
            return Ok("House number updated.");
        }

        [HttpPut("apartment-number")]
        public async Task<IActionResult> UpdateApartmentNumber([FromBody] string? number)
        {
            var userId = GetUserId();
            await _addressService.UpdateApartmentNumberAsync(userId, number);
            return Ok("Apartment number updated.");
        }
    }
}
