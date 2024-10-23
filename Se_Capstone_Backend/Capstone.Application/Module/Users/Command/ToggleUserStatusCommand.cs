using MediatR;

namespace Capstone.Application.Module.Users.Command
{
    public class ToggleUserStatusCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
    }

}
