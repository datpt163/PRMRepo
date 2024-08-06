using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Account.Command
{
    public class ResetPasswordCommand : IRequest<ResponseMediator>
    {
        public string? Code { get; set; }
        public string? NewPassword { get; set; }
    }
}
