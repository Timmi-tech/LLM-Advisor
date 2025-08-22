namespace Application.DTOs
{
    public class CreateFeedbackDto
    {
        public string Message { get; set; } = string.Empty;
        public int Rating { get; set; }
    }

    public class FeedbackResponseDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
    public class FeedbackStatisticsDto
    {
        public double AverageRating { get; set; }
        public int TotalFeedbackCount { get; set; }
    }
}