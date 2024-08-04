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
using Capstone.Infrastructure.DbContext;
namespace Capstone.Application.Module.Account.QueryHandle
{
    public class LoginQueryHandle : IRequestHandler<LoginQuery, ResponseMediator>
    {
        //private readonly IUnitOfWork _unitofwork;
        private readonly IJwtService _jwtService;

        public LoginQueryHandle(IJwtService jwtService)
        {
            //_unitofwork = unitofwork;
            _jwtService = jwtService;
        }
        public async Task<ResponseMediator> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email))
                return new ResponseMediator(false, "email is empty", null);

            if (string.IsNullOrEmpty(request.Password))
                return new ResponseMediator(false, "password is empty", null);
            //var ac = _unitofwork.Users.FindByCondition(s => s.Email.Equals(request.email) && s.Password.Equals(request.password)).Include(s => s.Role).FirstOrDefault();
            var ac = MyDbContext.Users.FirstOrDefault(a => (a.Email.Equals(request.Email) && a.Password.Equals(request.Password)));

            if (ac is null)
            {
                return new ResponseMediator(false, "account is not found", null);
            }

            return new ResponseMediator(true, "login succes", (await _jwtService.generatejwttokentw( ac) ));
        }
    }
}
