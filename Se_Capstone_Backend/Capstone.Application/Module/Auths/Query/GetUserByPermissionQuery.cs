using Capstone.Application.Common.ResponseMediator;
using MediatR;

namespace Capstone.Application.Module.Auths.Query
{
    public class GetUserByPermissionQuery : IRequest<ResponseMediator>
    {
        public string permissionName { get; set; } = string.Empty;
    }
}
