using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
