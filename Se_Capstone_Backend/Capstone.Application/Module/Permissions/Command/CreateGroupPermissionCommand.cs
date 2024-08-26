using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Permissions.Command
{
    public class CreateGroupPermissionCommand : IRequest<ResponseMediator>
    {
        public string Name { get; set; } = string.Empty;
    }
}
