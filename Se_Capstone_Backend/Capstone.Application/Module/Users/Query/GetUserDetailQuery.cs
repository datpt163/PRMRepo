using Capstone.Application.Module.Users.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.Query
{
    public class GetUserDetailQuery : IRequest<UserDto?>
    {
        public Guid UserId { get; set; }

        public GetUserDetailQuery(Guid userId) 
        {
            UserId = userId;
        }
    }
}