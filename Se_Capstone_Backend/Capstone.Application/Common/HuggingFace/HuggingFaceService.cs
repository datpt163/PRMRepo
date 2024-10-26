using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace Capstone.Application.Common.HuggingFace
{
    public class HuggingFaceService : IHuggingFaceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiToken;

        public HuggingFaceService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiToken = configuration["HuggingFace:ApiToken"] ?? string.Empty;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);
        }

        public async Task<string> GetResponseAsync(string prompt, string systemMessage = "You are a helpful assistant.", int maxTokens = 4096)
        {
            var requestBody = new
            {
                inputs = systemMessage + prompt,
                parameters = new { max_length = maxTokens }
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api-inference.huggingface.co/models/gpt2", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {response.StatusCode}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var huggingFaceResponse = JsonSerializer.Deserialize<HuggingFaceResponse>(responseJson);

            return huggingFaceResponse?.GeneratedText ?? "No response from Hugging Face";
        }
    }

    public class HuggingFaceResponse
    {
        [JsonPropertyName("generated_text")]
        public string GeneratedText { get; set; } = string.Empty;
    }
}
