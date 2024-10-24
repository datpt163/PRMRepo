using Capstone.Application.Common.Gpt;
using Capstone.Application.Module.Projects.Query;
using Capstone.Application.Module.Projects.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.QueryHandle
{
    public class SuggestProjectQueryHandler : IRequestHandler<SuggestProjectQuery, SuggestDto>
    {
        private readonly IChatGPTService _chatGptService;

        public SuggestProjectQueryHandler(IChatGPTService chatGptService)
        {
            _chatGptService = chatGptService;
        }

        public async Task<SuggestDto> Handle(SuggestProjectQuery request, CancellationToken cancellationToken)
        {
            var systemMessage = "You are a helpful assistant that provides potential employee suggestions based on project requirements. Please return the result as a JSON array of GUIDs.";
            var maxTokens = 3000;
            var requestJson = JsonSerializer.Serialize(request);
            var gptResponse = await _chatGptService.GetChatGptResponseAsync(requestJson, systemMessage, maxTokens);
            var potentialEmployees = ParseGptResponse(gptResponse);
            var suggestDto = new SuggestDto
            {
                UserId = potentialEmployees
            };
            return suggestDto;
        }
        private List<Guid> ParseGptResponse(string gptResponse)
        {
            return JsonSerializer.Deserialize<List<Guid>>(gptResponse) ?? new List<Guid>();
        }

    }
}
