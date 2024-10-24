
namespace Capstone.Application.Common.Gpt
{
    public interface IChatGPTService
    {
        Task<string> GetChatGptResponseAsync(string prompt, string systemMessage , int maxTokens);
    }
}
