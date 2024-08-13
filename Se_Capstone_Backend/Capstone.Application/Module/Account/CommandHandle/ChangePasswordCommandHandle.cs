using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Account.Command;
using Capstone.Infrastructure.DbContexts;
using Capstone.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Account.CommandHandle
{
    public class ChangePasswordCommandHandle : IRequestHandler<ChangePasswordCommand, ResponseMediator>
    {
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordCommandHandle(IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Token))
                return new ResponseMediator("Token is empty", null);

            if (request.NewPassword.Equals(request.OldPassword))
                return new ResponseMediator("New password is the same as the old password", null);

            var user = await _jwtService.VerifyToken(request.Token);

            if(user == null || user.Password is null)
                return new ResponseMediator("Account is invalid", null);

            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
            {
                return new ResponseMediator("Old password not correct", null);
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.Password = hashedPassword;
            _unitOfWork.Users.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", null);
        }
    }
}
