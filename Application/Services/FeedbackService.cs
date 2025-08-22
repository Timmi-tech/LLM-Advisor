using Application.DTOs;
using Application.Services.Contracts;
using Domain.Entities.Contracts;
using Domain.Entities.Models;

namespace Application.Services
{
    public class FeedbackService(IFeedBackRepository repository) : IFeedBackService
    {
        private readonly IFeedBackRepository _repository = repository;

        public async Task SubmitFeedbackAsync(CreateFeedbackDto feedback, string userId)
        {
            // business logic could go here (validation, notifications, etc.)
            await _repository.AddFeedbackAsync(new FeedBack
            {
                UserId = userId,
                Message = feedback.Message,
                Rating = feedback.Rating
            });
        }

        public async Task<IEnumerable<FeedbackResponseDto>> GetAllFeedbackAsync()
        {
            var feedbacks = await _repository.GetAllFeedbacksAsync();
            return feedbacks.Select(f => new FeedbackResponseDto
            {
                UserId = f.UserId,
                Message = f.Message,
                Rating = f.Rating,
                SubmittedAt = f.SubmittedAt
            });
        }

        public async Task<FeedbackStatisticsDto> GetAverageRatingAsync()
        {
            var averageRating = await _repository.GetAverageRatingAsync();
            var totalFeedbackCount = (await _repository.GetAllFeedbacksAsync()).Count;

            return new FeedbackStatisticsDto
            {
                AverageRating = averageRating,
                TotalFeedbackCount = totalFeedbackCount
            };
        }
    }
}