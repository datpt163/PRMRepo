using Capstone.Application.Common.Jwt;
using Capstone.Application.Module.Auths.Model;
using Capstone.Domain.Module.Auth.TokenBlackList;
using Capstone.Infrastructure.Redis;
using Microsoft.AspNetCore.Http;


namespace Capstone.Application.Common.TokenService
{
    public class TokenRevocationService : ITokenRevocationService
    {
        private readonly RedisContext _redis;
        private readonly ITokenBlacklistService _tokenBlacklistService;
        private readonly IJwtService _jwtService;

       public TokenRevocationService(RedisContext redis, ITokenBlacklistService tokenBlacklistService, IJwtService jwtService)
        {
            _redis = redis;
            _tokenBlacklistService = tokenBlacklistService;
            _jwtService = jwtService;
        }

        public async Task<(bool isSuccess, string errorMessage)> RevocationTokenAsync(Guid userId)
        {
            try 
            {
                var listMonitorToken = _redis.GetData<List<MonitorTokenModel>>("ListMonitorToken");
                if (listMonitorToken != null)
                {
                    var tokensToRemove = new List<MonitorTokenModel>();

                    foreach (var monitorToken in listMonitorToken)
                    {
                        var userCheck = await _jwtService.VerifyTokenAsync(monitorToken.Token);
                        if (userCheck != null && userCheck.Id == userId)
                        {
                            await _tokenBlacklistService.BlacklistTokenAsync(monitorToken.Token, StatusCodes.Status401Unauthorized);
                            tokensToRemove.Add(monitorToken);
                        }
                    }

                    foreach (var tokenToRemove in tokensToRemove)
                    {
                        listMonitorToken.Remove(tokenToRemove);
                    }

                    _redis.SetData<List<MonitorTokenModel>>("ListMonitorToken", listMonitorToken, DateTime.Now.AddYears(10));
                }
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
