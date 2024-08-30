using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.Command
{
    public class ChangePasswordCommand : IRequest<ResponseMediator>
    {
        public string Token { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;

        public ChangePasswordCommand(string token, string oldPassword, string newPassword)
        {
            Token = token;
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }
}
