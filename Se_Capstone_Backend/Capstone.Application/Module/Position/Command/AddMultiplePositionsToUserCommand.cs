using Capstone.Application.Module.Positions.Response;
using MediatR;


namespace Capstone.Application.Module.Positions.Command
{
    public class AddMultiplePositionsToUserCommand : IRequest<AddMultiplePositionsResponse>
    {
        public Guid UserId { get; set; }
        public List<Guid> PositionIds { get; set; } = new List<Guid>();

    }
}
