using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Auths.Command
{
    public class ToggleProjectCommand : IRequest<ResponseMediator>
    {
        public Guid Id { get; set; }
    }
}
