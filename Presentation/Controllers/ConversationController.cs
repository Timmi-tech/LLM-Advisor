using Application.Services.Contracts;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/conversation")]
    [Authorize]
    public class ConversationController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, ConversationState> _sessions = new();
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        /// <summary>
        /// Start a new conversation
        /// </summary>
        [HttpPost("start")]
        public ActionResult<ConversationStartResponse> Start()
        {
            var conversationId = Guid.NewGuid().ToString();
            _sessions[conversationId] = new ConversationState();

            return Ok(new ConversationStartResponse
            {
                ConversationId = conversationId,
                Question = "What is your previous degree?"
            });
        }

        /// <summary>
        /// Send user input and get next question or final recommendations
        /// </summary>
        [HttpPost("next")]
        public async Task<ActionResult<ConversationNextResponse>> Next([FromBody] ConversationRequest request)
        {
            if (request is null || string.IsNullOrWhiteSpace(request.ConversationId))
                return BadRequest("Conversation ID is required.");

            if (!_sessions.TryGetValue(request.ConversationId, out var state))
                return BadRequest("Invalid conversation ID");

            if (string.IsNullOrWhiteSpace(request.UserInput))
                return BadRequest("User input is required.");

            var nextQuestion = await _conversationService.GetNextQuestionAsync(request.UserInput, state);

            var recommendations = await _conversationService.GetFinalRecommendationAsync(state);

            return Ok(new ConversationNextResponse
            {
                NextQuestion = nextQuestion,
                Recommendations = recommendations
            });
        }
    }

    public class ConversationRequest
    {
        public string? ConversationId { get; set; }
        public string? UserInput { get; set; }
    }

    public class ConversationStartResponse
    {
        public string ConversationId { get; set; } = string.Empty;
        public string Question { get; set; } = string.Empty;
    }

    public class ConversationNextResponse
    {
        public string? NextQuestion { get; set; }
        public RecommendationResponse? Recommendations { get; set; }
    }
}
