using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Redis
{
    public class RedisContext 
    {
        private IDatabase _cacheDb;
        public RedisContext(IConnectionMultiplexer redis)
        {
            _cacheDb = redis.GetDatabase();
        }

        public T? GetData<T>(string key)
        {
            var value = _cacheDb.StringGet(key).ToString();
            if (!string.IsNullOrEmpty(value))
            {
                var response = JsonSerializer.Deserialize<T>(value);
                return response;
            }
            return default(T);
        }

        public bool SetData<T>(string key, T value, DateTime expirationTime)
        {
            var expirTime = expirationTime.Subtract(DateTime.Now);
            return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expirTime);
        }

        public object RemoveData(string key)
        {
            var _exist = _cacheDb.KeyExists(key);

            if (_exist)
                return _cacheDb.KeyDelete(key);

            return false;
        }
    }
}
