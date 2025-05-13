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

        /// <summary>
        /// Sukuria naują profilio įrašą su nuotrauka.
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateProfile([FromForm] PersonCreateForm form)
        {
            var userId = GetUserId();

            using var stream = form.ProfileImage.OpenReadStream();
            var imageBytes = await _imageService.ProcessImageAsync(stream, form.ProfileImage.FileName);

            var dto = new PersonCreateDto
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                PersonalCode = form.PersonalCode,
                PhoneNumber = form.PhoneNumber,
                Email = form.Email,
                ProfileImage = imageBytes,
                Address = new AddressDto
                {
                    City = form.City,
                    Street = form.Street,
                    HouseNumber = form.HouseNumber,
                    ApartmentNumber = form.ApartmentNumber
                }
            };

            await _personService.UploadPersonInfoAsync(userId, dto);
            return Ok("Profile created.");
        }

        /// <summary>
        /// Atnaujina profilio duomenis (be nuotraukos).
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromForm] PersonUpdateForm form)
        {
            var userId = GetUserId();

            var dto = new PersonUpdateDto
            {
                FirstName = form.FirstName,
                LastName = form.LastName,
                PhoneNumber = form.PhoneNumber,
                Email = form.Email,
                City = form.City,
                Street = form.Street,
                HouseNumber = form.HouseNumber,
                ApartmentNumber = form.ApartmentNumber
            };

            await _personService.UpdatePartialInfoAsync(userId, dto);

            return Ok("Profile updated.");
        }

        /// <summary>
        /// Atnaujina tik profilio nuotrauką.
        /// </summary>
        [HttpPut("profileimage")]
        public async Task<IActionResult> UpdateImage([FromForm] IFormFile file)
        {
            var userId = GetUserId();

            using var stream = file.OpenReadStream();
            var imageBytes = await _imageService.ProcessImageAsync(stream, file.FileName);
            await _personService.UpdateProfileImageAsync(userId, imageBytes);

            return Ok("Profile image updated.");
        }

        /// <summary>
        /// Gražina visą profilio informaciją.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            var user = await _personService.GetPersonByIdAsync(userId);
            return Ok(user);
        }
    }
}
