using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Labels.Query
{
    public class GetListLabelQuery : IRequest<ResponseMediator>
    {
        public Guid? projectId { get; set; }
    }
}
