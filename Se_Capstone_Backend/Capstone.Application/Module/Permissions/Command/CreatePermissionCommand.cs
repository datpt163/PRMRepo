using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Permissions.Command
{
    public class CreatePermissionCommand : IRequest<ResponseMediator>
    {
        public string Name { get; set; } = string.Empty;
        public Guid GroupPermissionId { get; set; }
    }
}
