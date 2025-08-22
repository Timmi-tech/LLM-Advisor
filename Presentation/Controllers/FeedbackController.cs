using System.Security.Claims;
using Application.DTOs;
using Application.Services.Contracts;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/feedback")]
    [Authorize]
    public class FeedbackController(IFeedBackService service) : ControllerBase
    {
        private readonly IFeedBackService _service = service;

        [HttpPost]
        public async Task<IActionResult> SubmitFeedback([FromBody] CreateFeedbackDto feedback)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null)
                return Unauthorized();

            await _service.SubmitFeedbackAsync(feedback, userId);
            return StatusCode(201);  
        }
        [HttpGet]
        public async Task<IActionResult> GetAllFeedback()
        {
            var feedbacks = await _service.GetAllFeedbackAsync();
            return Ok(feedbacks);
        }

        [HttpGet("average-rating")]
        public async Task<IActionResult> GetAverageRating()
        {
            var stats = await _service.GetAverageRatingAsync();
            return Ok(stats);
        }
        private string? GetUserIdFromClaims()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}