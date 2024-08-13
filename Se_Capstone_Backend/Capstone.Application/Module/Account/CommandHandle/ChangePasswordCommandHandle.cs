using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Account.Command;
using Capstone.Infrastructure.DbContexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Account.CommandHandle
{
    public class ChangePasswordCommandHandle : IRequestHandler<ChangePasswordCommand, ResponseMediator>
    {
        private readonly IJwtService _jwtService;

        public ChangePasswordCommandHandle(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<ResponseMediator> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.OldPassword))
                return new ResponseMediator("OldPassword is empty", null);

            if (string.IsNullOrEmpty(request.NewPassword))
                return new ResponseMediator("NewPassword is empty", null);

            if (string.IsNullOrEmpty(request.Token))
                return new ResponseMediator("Token is empty", null);

            if (request.NewPassword.Equals(request.OldPassword))
                return new ResponseMediator("New password is the same as the old password", null);

            var ac = await _jwtService.VerifyToken(request.Token);

            if(ac == null || ac.Password is null)
                return new ResponseMediator("Account is invalid", null);

            if(!ac.Password.Equals(request.OldPassword))
                return new ResponseMediator("Old password not correct", null);

            //change new pass
            return new ResponseMediator("", null);
        }
    }
}
