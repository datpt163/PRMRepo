using System.Threading.Tasks;

namespace Capstone.Application.Common.Cohere
{
    public interface ICohereService
    {
        Task<string> GetResponseAsync(string requestJson, string systemMessage, int maxTokens);
    }
}
