using Domain.Entities.Models;

namespace Domain.Entities.Contracts
{
    public interface IFeedBackRepository
    {
        Task AddFeedbackAsync(FeedBack feedback);
        Task<List<FeedBack>> GetAllFeedbacksAsync();
        Task<double> GetAverageRatingAsync();
        Task<int> GetTotalFeedbackCountAsync();
        Task<Dictionary<int, int>> GetRatingDistributionAsync();
        Task<List<FeedBack>> GetRecentFeedbacksAsync(int count = 10);
        Task<(List<FeedBack> feedbacks, int totalCount)> GetPaginatedRecentFeedbacksAsync(int pageNumber, int pageSize);
        Task<List<(string Month, int Count, double AverageRating)>> GetMonthlyTrendsAsync(int months = 12);
    }
}