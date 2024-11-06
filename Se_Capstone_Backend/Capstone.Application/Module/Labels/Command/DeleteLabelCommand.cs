using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Labels.Command
{
    public class DeleteLabelCommand : IRequest<ResponseMediator>
    {
        public Guid Id { get; set; }
    }
}
