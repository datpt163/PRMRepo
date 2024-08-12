using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Profile.Command;
using Capstone.Infrastructure.DbContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Profile.CommandHandle
{
    public class UpdateProfileCommandHandle : IRequestHandler<UpdateProfileCommand, ResponseMediator>
    {
        private readonly IJwtService _jwtService;

        public UpdateProfileCommandHandle(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }
        public async Task<ResponseMediator> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var account = await _jwtService.VerifyToken(request.Token);
            if(account == null)
                return new ResponseMediator("Token wrongs", null);

            foreach(var acc in MyDbContext.Users)
            {
                if(acc.Id == account.Id)
                {
                    acc.FirstName = request.FirstName;
                    acc.LastName = request.LastName;
                }
            }
            return new ResponseMediator("", null);
        }
    }
}
