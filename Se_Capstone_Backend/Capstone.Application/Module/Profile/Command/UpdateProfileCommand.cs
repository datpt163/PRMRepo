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
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public UpdateProfileCommand(string token, string firstName, string lastName)
        {
            Token = token;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
