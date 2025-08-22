using Domain.Entities.Models;

namespace Application.Services.Contracts
{
    public interface IConversationService
{
    Task<string> GetNextQuestionAsync(string userInput, ConversationState state);
    Task<RecommendationResponse?> GetFinalRecommendationAsync(ConversationState state);
}
}