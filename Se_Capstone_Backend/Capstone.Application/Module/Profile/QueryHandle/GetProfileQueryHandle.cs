using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Profile.Query;
using Capstone.Application.Module.Profile.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Profile.QueryHandle
{
    public class GetProfileQueryHandle : IRequestHandler<GetProfileQuery, ResponseMediator>
    {
        private readonly IJwtService _jwtService;

        public  GetProfileQueryHandle(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }
        public async Task<ResponseMediator> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var account = await _jwtService.VerifyToken(request.Token);
            if (account == null)
                return new ResponseMediator("Token wrongs", null);

            return new ResponseMediator("", new UpdateUserResponse(account.Id, account.Email, account.PhoneNumber, account.FullName, account.Avatar, account.CreateDate));
        }
    }
}
