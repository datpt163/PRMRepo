using Capstone.Application.Common.ResponseMediator;
using MediatR;

namespace Capstone.Application.Module.Status.Command
{
    public class DeleteStatusDefaultCommand : IRequest<ResponseMediator>
    {
        public Guid Id { get; set; }
    }
}
