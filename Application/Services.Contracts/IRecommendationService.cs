using Domain.Entities.Models;

namespace Application.Services.Contracts
{
    public interface IRecommendationService
    {
        Task<RecommendationResponse> GetRecommendationsAsync(StudentProfile student);
    }
}