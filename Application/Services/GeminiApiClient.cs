using System.Net.Http.Json;
using System.Text.Json;
using Domain.Entities.ConfigurationsModels;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class GeminiApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiApiClient(HttpClient httpClient, IOptions<GeminiSettings> settings)
        {
            _httpClient = httpClient;
            _apiKey = settings.Value.ApiKey;
        }

        public async Task<string> GenerateContentAsync(string prompt)
        {
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

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={_apiKey}";

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Gemini API call failed: {response.StatusCode}");

            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

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
