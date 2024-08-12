using Capstone.Application.Common.Email;
using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Account.Model;
using Capstone.Application.Module.Account.Query;
using Capstone.Infrastructure.DbContext;
using Capstone.Infrastructure.Redis;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Account.QueryHandle
{
    public class RegisterQueryHandle : IRequestHandler<RegisterQuery, ResponseMediator>
    {
        private readonly IEmailService _emailService;
        private readonly RedisContext _redisContext;

        public RegisterQueryHandle(IEmailService emailService, RedisContext redisContext)
        {
            _emailService = emailService;
            _redisContext = redisContext;
        }
        public async Task<ResponseMediator> Handle(RegisterQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email))
                return new ResponseMediator("Email is empty", null);
            if (string.IsNullOrEmpty(request.Password))
                return new ResponseMediator("Password is empty", null);
            if (string.IsNullOrEmpty(request.FirstName))
                return new ResponseMediator("FirstName is empty", null);
            if (string.IsNullOrEmpty(request.LastName))
                return new ResponseMediator("LastName is empty", null);
            var acc = MyDbContext.Users.FirstOrDefault(s => s.Email.Equals(request.Email));
            if(acc != null)
                return new ResponseMediator("Account is already exists ", null);

            Random random = new Random();
            int randomNumber = random.Next(100000, 1000000);
            (bool isSuccess, string message) = await _emailService.SendEmailAsync(request.Email, "Register OTP", "Here is your register verification code. Please enter it soon before it expires in 5 minutes: " + randomNumber);
            if (!isSuccess)
            {
                return new ResponseMediator("message", null);
            }
            _redisContext.SetData("OtpRegister-" + request.Email, new RegisterRedisData( randomNumber,request.Password,request.FirstName,request.LastName), DateTime.Now.AddMinutes(5));
            return new ResponseMediator("", null);
        }
    }
}
