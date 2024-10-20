using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.Command
{
    public class ToggleUserStatusCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
    }

}
