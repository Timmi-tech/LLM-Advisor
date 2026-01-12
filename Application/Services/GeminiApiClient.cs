using System.Net.Http.Json;
using System.Text.Json;
using Domain.Entities.ConfigurationsModels;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class GeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<GeminiSettings> _settings;
        private readonly ILogger<GeminiApiClient> _logger;

        public GeminiApiClient(HttpClient httpClient, IOptionsMonitor<GeminiSettings> settings, ILogger<GeminiApiClient> logger)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }

        public async Task<string> GenerateContentAsync(string prompt)
        {
            var apiKey = _settings.CurrentValue.ApiKey;

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                _logger.LogError("Gemini API key is not configured.");
                throw new InvalidOperationException("Gemini API key is missing.");
            }

            var requestBody = new
            {
                contents = new[]
                {
                    new {
                        role = "user",
                        parts = new[] { new { text = prompt } }
                    }
                }
            };

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsJsonAsync(url, requestBody);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to reach Gemini API.");
                throw new Exception("Could not reach Gemini API.", ex);
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                if (responseContent.Contains("API key expired"))
                {
                    _logger.LogWarning("Gemini API key expired. Please update your API key.");
                }
                else
                {
                    _logger.LogError("Gemini API error: {StatusCode}, Response: {Response}", response.StatusCode, responseContent);
                }

                throw new Exception($"Gemini API request failed: {responseContent}");
            }

            var result = JsonSerializer.Deserialize<JsonElement>(responseContent);

            var content = result
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return content ?? string.Empty;
        }
    }
}
