using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Profile.Command;
using Capstone.Application.Module.Profile.Response;
using Capstone.Infrastructure.DbContexts;
using Capstone.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Profile.CommandHandle
{
    public class UpdateProfileCommandHandle : IRequestHandler<UpdateProfileCommand, ResponseMediator>
    {
        private readonly IJwtService _jwtService;
        private readonly SeCapstoneContext _ct;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProfileCommandHandle(IJwtService jwtService, SeCapstoneContext ct, IUnitOfWork unitOfWok)
        {
            _jwtService = jwtService;
            _unitOfWork = unitOfWok;
            _ct = ct;   
        }
        public async Task<ResponseMediator> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var account = await _jwtService.VerifyToken(request.Token);
            if(account == null)
                return new ResponseMediator("Token wrongs", null);

            account.FullName = request.FullName;
            account.PhoneNumber = request.Phone;
            account.Avatar = request.Avatar + "_" + Guid.NewGuid();

            _unitOfWork.Users.Update(account);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", new UpdateUserResponse(account.Id, account.Email, account.PhoneNumber, account.FullName, account.Avatar, account.CreateDate));
        }
    }
}
