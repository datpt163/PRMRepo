using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Auths.Command
{
    public class AddRoleForUserCommand : IRequest<ResponseMediator>
    {
        public Guid UserId { get; set; } 
        public Guid RoleId { get; set; } 
    }
}
