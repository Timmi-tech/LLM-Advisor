namespace Application.DTOs
{
    public class FeedbackAnalyticsDto
    {
        public int TotalFeedbacks { get; set; }
        public double AverageRating { get; set; }
        public RatingDistributionDto RatingDistribution { get; set; } = new();
        public List<MonthlyFeedbackDto> MonthlyTrends { get; set; } = new();
        public List<TopFeedbackDto> RecentFeedbacks { get; set; } = new();
        public FeedbackSummaryDto Summary { get; set; } = new();
    }

    public class RatingDistributionDto
    {
        public int OneStar { get; set; }
        public int TwoStar { get; set; }
        public int ThreeStar { get; set; }
        public int FourStar { get; set; }
        public int FiveStar { get; set; }
    }

    public class MonthlyFeedbackDto
    {
        public string Month { get; set; } = string.Empty;
        public int Count { get; set; }
        public double AverageRating { get; set; }
    }

    public class TopFeedbackDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; } = string.Empty;
    }

    public class FeedbackSummaryDto
    {
        public int HighRatings { get; set; } // 4-5 stars
        public int MediumRatings { get; set; } // 3 stars
        public int LowRatings { get; set; } // 1-2 stars
        public double PositivePercentage { get; set; }
        public double SatisfactionScore { get; set; }
    }
}