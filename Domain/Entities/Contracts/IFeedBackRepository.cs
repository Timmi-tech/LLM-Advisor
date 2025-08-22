using Domain.Entities.Models;

namespace Domain.Entities.Contracts
{
    public interface IFeedBackRepository
    {
        Task AddFeedbackAsync(FeedBack feedback);
        Task<List<FeedBack>> GetAllFeedbacksAsync();
        Task<double> GetAverageRatingAsync();
    }

}