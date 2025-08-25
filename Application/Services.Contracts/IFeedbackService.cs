using Application.DTOs;
using Domain.Entities.Models;

namespace Application.Services.Contracts
{
    public interface IFeedBackService
    {
        Task SubmitFeedbackAsync(CreateFeedbackDto feedback,string userId);
        Task<IEnumerable<FeedbackResponseDto>> GetAllFeedbackAsync();
        Task<FeedbackStatisticsDto> GetAverageRatingAsync();
        Task<FeedbackAnalyticsDto> GetFeedbackAnalyticsAsync();
    }
}