using Capstone.Application.Common.ResponseMediator;
using MediatR;

namespace Capstone.Application.Module.Status.Query
{
    public class GetListStatusQuery : IRequest<ResponseMediator>
    {
        public Guid? projectId { get; set; }
    }
}
