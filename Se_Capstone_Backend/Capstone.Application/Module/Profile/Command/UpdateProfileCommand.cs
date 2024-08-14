using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Profile.Command
{
    public class UpdateProfileCommand : IRequest<ResponseMediator>
    {
        public string Token { get; set; } = string.Empty;
        public string Phone { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Avatar { get; set; } = null!;

        public UpdateProfileCommand(string token, string fullName, string phone, string avatar)
        {
            Token = token;
            Phone = phone;
            FullName = fullName;
            Avatar = avatar;
        }
    }
}
