using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auths.Query;
using Capstone.Application.Module.Auths.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.QueryHandle
{
    public class ProfileQueryHandle : IRequestHandler<ProfileQuery, ResponseMediator>
    {
        private readonly IJwtService _jwtService;
        public ProfileQueryHandle(IJwtService jwtService )
        {
            _jwtService = jwtService;
        }
        public async Task<ResponseMediator> Handle(ProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.VerifyTokenAsync(request.Token);

            if (user == null)
            {
                return new ResponseMediator("Account not found", null);
            }
            var responseUser = new RegisterResponse(user.Status, user.Email ?? "", user.Id, user.UserName ?? "", user.FullName, user.PhoneNumber ?? "", user.Avatar ?? "",
                                          user.Address ?? "", user.Gender, user.Dob, user.BankAccount, user.BankAccountName,
                                          user.CreateDate, user.UpdateDate, user.DeleteDate);

            return new ResponseMediator("", responseUser);
        }
    }
}
