using MediatR;

namespace Capstone.Application.Module.Positions.Command
{
    public class DeletePositionCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
