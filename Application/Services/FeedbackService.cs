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

        public async Task<FeedbackAnalyticsDto> GetFeedbackAnalyticsAsync()
        {
            var totalFeedbacks = await _repository.GetTotalFeedbackCountAsync();
            var averageRating = totalFeedbacks > 0 ? await _repository.GetAverageRatingAsync() : 0;
            var ratingDistribution = await _repository.GetRatingDistributionAsync();
            var monthlyTrends = await _repository.GetMonthlyTrendsAsync();
            var recentFeedbacks = await _repository.GetRecentFeedbacksAsync();

            var analytics = new FeedbackAnalyticsDto
            {
                TotalFeedbacks = totalFeedbacks,
                AverageRating = Math.Round(averageRating, 2),
                RatingDistribution = new RatingDistributionDto
                {
                    OneStar = ratingDistribution.GetValueOrDefault(1, 0),
                    TwoStar = ratingDistribution.GetValueOrDefault(2, 0),
                    ThreeStar = ratingDistribution.GetValueOrDefault(3, 0),
                    FourStar = ratingDistribution.GetValueOrDefault(4, 0),
                    FiveStar = ratingDistribution.GetValueOrDefault(5, 0)
                },
                MonthlyTrends = monthlyTrends.Select(t => new MonthlyFeedbackDto
                {
                    Month = t.Month,
                    Count = t.Count,
                    AverageRating = Math.Round(t.AverageRating, 2)
                }).ToList(),
                RecentFeedbacks = recentFeedbacks.Select(f => new TopFeedbackDto
                {
                    Id = f.Id,
                    Rating = f.Rating,
                    Comment = f.Message,
                    CreatedAt = f.CreatedAt,
                    UserName = f.User?.FirstName + " " + f.User?.LastName ?? "Anonymous"
                }).ToList()
            };

            // Calculate summary statistics
            var highRatings = ratingDistribution.GetValueOrDefault(4, 0) + ratingDistribution.GetValueOrDefault(5, 0);
            var mediumRatings = ratingDistribution.GetValueOrDefault(3, 0);
            var lowRatings = ratingDistribution.GetValueOrDefault(1, 0) + ratingDistribution.GetValueOrDefault(2, 0);

            analytics.Summary = new FeedbackSummaryDto
            {
                HighRatings = highRatings,
                MediumRatings = mediumRatings,
                LowRatings = lowRatings,
                PositivePercentage = totalFeedbacks > 0 ? Math.Round((double)highRatings / totalFeedbacks * 100, 1) : 0,
                SatisfactionScore = Math.Round(averageRating / 5 * 100, 1)
            };

            return analytics;
        }
    }
}