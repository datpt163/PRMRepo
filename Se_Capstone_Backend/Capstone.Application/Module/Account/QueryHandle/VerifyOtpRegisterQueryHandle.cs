using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Account.Model;
using Capstone.Application.Module.Account.Query;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContexts;
using Capstone.Infrastructure.Redis;
using Capstone.Infrastructure.Repository;
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
        private readonly IUnitOfWork _unitOfWork;

        public VerifyOtpRegisterQueryHandle(RedisContext redisContext, IUnitOfWork unitOfWork)
        {
            _redisContext = redisContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(VerifyOtpRegisterQuery request, CancellationToken cancellationToken)
        {
            var infor = _redisContext.GetData<RegisterRedisData>("OtpRegister-" + request.Email);
            if (infor == null)
                return new ResponseMediator("Otp expired or not sent", null);
            if (infor.Otp != request.Otp)
                return new ResponseMediator("Otp is not correct", null);

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(infor.Password);
            _unitOfWork.Users.Add(new User(request.Email, hashedPassword, infor.Phone, infor.Fullname, infor.Avatar ));
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", null);
        }
    }
}
