using System.Security.Claims;
using Application.DTOs;
using Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized();

            var profile = await _userService.GetUserProfileAsync(userId);
            if (profile == null) return NotFound();

            return Ok(profile);
        }

        [HttpPut("profile")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto updateDto)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized();

            var updatedProfile = await _userService.UpdateUserProfileAsync(userId, updateDto);
            return Ok(updatedProfile);
        }

        [HttpPut("change-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized();

            try
            {
                await _userService.ChangePasswordAsync(userId, changePasswordDto);
                return Ok(new { message = "Password changed successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private string? GetUserIdFromClaims()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}