using MediatR;


namespace Capstone.Application.Module.Positions.Command
{
    public class AddPositionToUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid PositionId { get; set; }
    }

}
