using MediatR;

namespace Capstone.Application.Module.Jobs.Command
{
    public class DeleteJobCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
