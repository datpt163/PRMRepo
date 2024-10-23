using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Labels.Command
{
    public class DeleteDefaultLabelCommand : IRequest<ResponseMediator>
    {
        public Guid Id { get; set; }
    }
}
