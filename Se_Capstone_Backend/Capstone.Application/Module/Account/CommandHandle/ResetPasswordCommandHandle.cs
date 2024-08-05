using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Account.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Account.CommandHandle
{
    public class ResetPasswordCommandHandle : IRequestHandler<ResetPasswordCommand, ResponseMediator>
    {
        private readonly IJwtService _jwtService;

        public ResetPasswordCommandHandle(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<ResponseMediator> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.NewPassword))
                return new ResponseMediator("Password is null or empty", null);

            if (string.IsNullOrEmpty(request.Code))
                return new ResponseMediator("Code is null or empty", null);

            var ac = await _jwtService.VerifyToken(request.Code);

            if(ac == null)
                return new ResponseMediator("Reset password is expire, you must resend emai", null);

            return new ResponseMediator("", null);
        }
    }
}
