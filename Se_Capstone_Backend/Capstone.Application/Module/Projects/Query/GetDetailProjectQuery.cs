using Capstone.Application.Common.ResponseMediator;
using MediatR;

namespace Capstone.Application.Module.Projects.Query
{
    public class GetDetailProjectQuery : IRequest<ResponseMediator>
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
