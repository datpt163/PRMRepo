using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Account.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContexts;
using Capstone.Application.Module.Account.Response;
using Capstone.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
namespace Capstone.Application.Module.Account.QueryHandle
{
    public class LoginQueryHandle : IRequestHandler<LoginQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IJwtService _jwtService;

        public LoginQueryHandle(IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
            _jwtService = jwtService;
        }
        public async Task<ResponseMediator> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email))
                return new ResponseMediator("email is empty", null);

            if (string.IsNullOrEmpty(request.Password))
                return new ResponseMediator("password is empty", null);
            var account = await _unitofwork.Users.Find(s => s.Email.Equals(request.Email) && s.Password.Equals(request.Password)).FirstOrDefaultAsync();

            if (account is null)
            {
                return new ResponseMediator("account is not found", null);
            }
            var accessToken =  _jwtService.GenerateJwtToken(account);
            var refreshToken = "";
            return new ResponseMediator("", new LoginResponse() { AccessToken = accessToken, RefreshToken = refreshToken } );
        }
    }
}
