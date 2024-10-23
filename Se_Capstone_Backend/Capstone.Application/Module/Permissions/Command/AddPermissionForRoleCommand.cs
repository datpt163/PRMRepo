using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Permissions.Command
{
    public class AddPermissionForRoleCommand : IRequest<ResponseMediator>
    {
        public Guid RoleId { get; set; }
        public List<PermissionRequest> Permissions { get; set; } = new List<PermissionRequest>();
    }

    public class PermissionRequest
    {
        public Guid PermissionId { get; set; } 
    }
}
