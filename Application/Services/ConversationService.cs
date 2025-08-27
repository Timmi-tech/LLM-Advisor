using Application.Services.Contracts;
using Domain.Entities.Models;
using Domain.Entities.Enums;

namespace Application.Services
{
    public class ConversationService(IRecommendationService recommendationService) : IConversationService
    {
        private readonly IRecommendationService _recommendationService = recommendationService;

        public async Task<string> GetNextQuestionAsync(string userInput, ConversationState state)
        {
            switch (state.Step)
            {
                case 0:
                    state.Student.PreviousDegree = userInput;
                    state.Step++;
                    return "What is your performance level (e.g., First Class, Second Class Upper)?";

                case 1:
                    if (Enum.TryParse<AcademicPerformanceLevel>(userInput.Replace(" ", ""), true, out var performanceLevel))
                        state.Student.PerformanceLevel = performanceLevel;
                    state.Step++;
                    return "What are your academic interests? (separate with commas)";

                case 2:
                    state.Student.AcademicInterests = userInput.Split(',').Select(x => x.Trim()).ToList();
                    state.Step++;
                    return "Enter your preferred course of study, if any (e.g., MSc, MBA, PhD) - Leave blank if unsure";

                case 3:
                    state.Student.DesiredPrograms = userInput.Split(',').Select(x => x.Trim()).ToList();
                    state.Step++;
                    return "Thanks! Generating your recommendations...";

                default:
                    return "All questions answered.";
            }
        }

        public async Task<RecommendationResponse?> GetFinalRecommendationAsync(ConversationState state)
        {
            if (state.Step < 4)
                return null; // Not ready yet
            return await _recommendationService.GetRecommendationsAsync(state.Student);
        }
    }
}