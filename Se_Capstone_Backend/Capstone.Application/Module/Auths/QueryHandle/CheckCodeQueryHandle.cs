using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auths.Query;
using Capstone.Infrastructure.Redis;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.QueryHandle
{
    public class CheckCodeQueryHandle : IRequestHandler<CheckCodeQuery, ResponseMediator>
    {
        private readonly IJwtService _jwtService;
        private readonly RedisContext _redisContext;

        public CheckCodeQueryHandle(IJwtService jwtService, RedisContext redisContext)
        {
            _jwtService = jwtService;
            _redisContext = redisContext;
        }
        public async Task<ResponseMediator> Handle(CheckCodeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var blackList = _redisContext.GetData<List<string>>("BlackListForgotPasswordCode");

                if (blackList != null)
                    if (blackList.Contains(request.Code))
                        return new ResponseMediator("This code has already been used", null);

                try
                {
                    var ac = await _jwtService.VerifyTokenAsync(request.Code, "da-32-character-ultra-secure-and-ultra-long-secret");
                    return new ResponseMediator("", null);
                }
                catch
                {
                    return new ResponseMediator("This code was expired or wrong", null);
                }

            }
            catch
            {
                return new ResponseMediator("", null);
            }
          
        }
    }
}
