using Capstone.Infrastructure.Redis;
using System;
using System.Threading.Tasks;

namespace Capstone.Domain.Module.Auth.TokenBlackList
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly RedisContext _redisContext;
        private readonly TimeSpan _tokenExpiryDuration = TimeSpan.FromDays(30);

        public TokenBlacklistService(RedisContext redisContext)
        {
            _redisContext = redisContext;
        }

        public async Task<bool> BlacklistTokenAsync(string token)
        {
            var key = GetRedisKey(token);
            var result = await Task.Run(() => _redisContext.SetData(key, true, DateTime.UtcNow.Add(_tokenExpiryDuration)));
            return result;
        }

        public async Task<bool> IsTokenBlacklistedAsync(string token)
        {
            var key = GetRedisKey(token);
            return await Task.Run(() => _redisContext.GetData<string>(key) != null);
        }

        private static string GetRedisKey(string token)
        {
            return $"blacklisted_token:{token}";
        }
    }
}
