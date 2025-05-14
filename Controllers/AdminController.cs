using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using UserRegistrations.Application.Interfaces;
using UserRegistrations.Domain.Enums;
using UserRegistrations.Domain.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;
    private readonly Guid _protectedAdminId;

    public AdminController(IUserRepository userRepository, IUserService userService, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _userService = userService;
        var adminIdString = configuration["AdminSeed:Id"];
        if (string.IsNullOrWhiteSpace(adminIdString))
            throw new Exception("AdminSeed:Id is not configured.");
        _protectedAdminId = Guid.Parse(adminIdString);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllAsync(); 

        var result = users.Select(u => new
        {
            u.Id,
            u.Username,
            Role = u.Role.ToString(),
            HasProfile = u.Person != null
        });

        return Ok(result);
    }

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetUserProfile(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null || user.Person == null)
            return NotFound("User or profile not found");

        var person = user.Person;
        return Ok(new
        {
            person.FirstName,
            person.LastName,
            person.PersonalCode,
            person.PhoneNumber,
            person.Email,
            Address = new
            {
                person.Address.City,
                person.Address.Street,
                person.Address.HouseNumber,
                person.Address.ApartmentNumber
            }
        });
    }

    [HttpPut("user/{id}/role")]
    public async Task<IActionResult> ChangeUserRole(Guid id, [FromBody] string role)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return NotFound("User not found");

        if (!Enum.TryParse<RoleType>(role, true, out var parsedRole))
            return BadRequest("Invalid role type");

        user.Role = parsedRole;
        await _userRepository.SaveChangesAsync();

        return Ok($"User role updated to {parsedRole}");
    }

    [HttpDelete("user/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
