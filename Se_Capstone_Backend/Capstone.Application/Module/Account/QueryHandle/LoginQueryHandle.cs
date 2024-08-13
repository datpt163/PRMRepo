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
            var account = await _unitofwork.Users.Find(s => s.Email.Equals(request.Email)).FirstOrDefaultAsync();
            if (account is null)
            {
                return new ResponseMediator("Account is not found", null);
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, account.Password))
            {
                return new ResponseMediator("Wrong password", null);
            }
            var accessToken =  _jwtService.GenerateJwtToken(account);
            var refreshToken = _jwtService.GenerateJwtToken(account, 15);
            return new ResponseMediator("", new LoginResponse() { AccessToken = accessToken, RefreshToken = refreshToken } );
        }
    }
}
