using MediatR;


namespace Capstone.Application.Module.Positions.Command
{
    public class RemovePositionsFromUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public List<Guid> PositionIds { get; set; } = new List<Guid>();
    }

}
