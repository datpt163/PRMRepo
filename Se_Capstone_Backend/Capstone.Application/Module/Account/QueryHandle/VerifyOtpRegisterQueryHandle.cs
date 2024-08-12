using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Account.Model;
using Capstone.Application.Module.Account.Query;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContext;
using Capstone.Infrastructure.Redis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Account.QueryHandle
{
    public class VerifyOtpRegisterQueryHandle : IRequestHandler<VerifyOtpRegisterQuery, ResponseMediator>
    {
        private readonly RedisContext _redisContext;

        public VerifyOtpRegisterQueryHandle(RedisContext redisContext)
        {
            _redisContext = redisContext;
        }

        public async Task<ResponseMediator> Handle(VerifyOtpRegisterQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email))
                return new ResponseMediator("Email is empty", null);

            var infor = _redisContext.GetData<RegisterRedisData>("OtpRegister-" + request.Email);
            if (infor == null)
                return new ResponseMediator("Otp is expire", null);
            if (infor.Otp != request.Otp)
                return new ResponseMediator("Otp is not correct", null);

            MyDbContext.Users.Add(new User() { Email = request.Email, Password = infor.Password, FirstName = infor.FirstName, LastName = infor.LastName });
            return new ResponseMediator("", null);
        }
    }
}
