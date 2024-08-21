using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.Command
{
    public class CreateRoleCommand : IRequest<ResponseMediator>
    {
        public string RoleName { get; set; } = string.Empty;
    }
}
