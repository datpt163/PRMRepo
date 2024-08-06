using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Account.Command
{
    public class ChangePasswordCommand : IRequest<ResponseMediator>
    {
        public string? token { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
