using Capstone.Application.Common.ResponseMediator;
using MediatR;

namespace Capstone.Application.Module.Status.Command
{
    public class DeleteStatusCommand : IRequest<ResponseMediator>
    {
        public Guid Id { get; set; }
        public Guid? NewStatusId { get; set; }
    }
}
