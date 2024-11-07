using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Capstone.Application.Common.Cohere
{
    public class CohereService : ICohereService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;

        public CohereService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = !string.IsNullOrEmpty(configuration["Cohere:ApiKey"])? configuration["Cohere:ApiKey"]: Environment.GetEnvironmentVariable("COHERE_API_KEY") ?? string.Empty;
            Console.WriteLine($"API Key from Configuration: {_apiKey}"); 
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GetResponseAsync(string requestJson, string systemMessage, int maxTokens)
        {
            var requestBody = new
            {
                prompt = systemMessage + requestJson,
                max_tokens = maxTokens
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.cohere.ai/generate", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {response.StatusCode}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var cohereResponse = JsonSerializer.Deserialize<CohereResponse>(responseJson, options);

            return cohereResponse?.Text ?? "No response from Cohere";
        }
    }

    public class CohereResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public Meta Meta { get; set; } = new Meta();
    }

    public class Meta
    {
        public ApiVersion ApiVersion { get; set; } = new ApiVersion();
        public List<string> Warnings { get; set; } = new List<string>();
        public BilledUnits BilledUnits { get; set; } = new BilledUnits();
    }

    public class ApiVersion
    {
        public string Version { get; set; } = string.Empty;
        public bool IsDeprecated { get; set; }
    }

    public class BilledUnits
    {
        public int InputTokens { get; set; }
        public int OutputTokens { get; set; }
    }

}
