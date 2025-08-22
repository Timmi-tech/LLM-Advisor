using System.Text.Json;
using Domain.Entities.Models;

namespace Application.Helpers
{
    public static class LlmResponseParser
    {
        public static bool TryParseRecommendations(string content, out List<RecommendationResult> results)
        {
            results = [];

            try
            {
                results = JsonSerializer.Deserialize<List<RecommendationResult>>(content) ?? [];
                return true;
            }
            catch
            {
                var extracted = ExtractJson(content);
                if (!string.IsNullOrEmpty(extracted))
                {
                    try
                    {
                        results = JsonSerializer.Deserialize<List<RecommendationResult>>(extracted) ?? [];
                        return results.Count != 0;
                    }
                    catch { }
                }
            }

            return false;
        }

        private static string? ExtractJson(string text)
        {
            var start = text.IndexOf('[');
            var end = text.LastIndexOf(']');
            return (start >= 0 && end > start) ? text[start..(end + 1)] : null;
        }
    }
}
