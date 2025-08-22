namespace Domain.Entities.Models
{
    public class RecommendationResult
    {
        public string ProgramName { get; set; } = string.Empty;
        public string University { get; set; } = "University of Lagos";
        public string Reason { get; set; } = string.Empty;
    }

    public class RecommendationResponse
    {
        public List<RecommendationResult> Recommendations { get; set; } = [];
    }
}