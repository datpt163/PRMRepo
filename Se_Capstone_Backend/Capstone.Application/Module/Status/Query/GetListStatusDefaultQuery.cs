using Capstone.Application.Common.ResponseMediator;
using MediatR;

namespace Capstone.Application.Module.Status.Query
{
    public class GetListStatusDefaultQuery : IRequest<ResponseMediator>
    {
    }
}
