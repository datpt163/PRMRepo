using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Profile.Command;
using Capstone.Infrastructure.DbContexts;
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
        private readonly SeCapstoneContext _ct;

        public UpdateProfileCommandHandle(IJwtService jwtService, SeCapstoneContext ct)
        {
            _jwtService = jwtService;
            _ct = ct;   
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
                    acc.Fullname = request.FirstName;
                }
            }
            return new ResponseMediator("", null);
        }
    }
}
