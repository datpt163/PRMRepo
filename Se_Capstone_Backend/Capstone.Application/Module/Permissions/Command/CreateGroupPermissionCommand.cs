using Capstone.Application.Common.ResponseMediator;
using MediatR;


namespace Capstone.Application.Module.Permissions.Command
{
    public class CreateGroupPermissionCommand : IRequest<ResponseMediator>
    {
        public string Name { get; set; } = string.Empty;
    }
}
