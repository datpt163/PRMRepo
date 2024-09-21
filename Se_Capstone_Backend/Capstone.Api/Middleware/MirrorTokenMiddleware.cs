using Capstone.Api.Common.Constant;
using Capstone.Application.Module.Auths.Model;
using Capstone.Infrastructure.Redis;
using MediatR;
using StackExchange.Redis;

namespace Capstone.Api.Middleware
{
    public class MirrorTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RedisContext _redis;

        public MirrorTokenMiddleware(RequestDelegate next, RedisContext reid)
        {
            _next = next;
            _redis = reid;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var tokenHeader))
            {
                var token = tokenHeader.ToString().Replace("Bearer ", "").Trim();
                var listMonitorToken = _redis.GetData<List<MonitorTokenModel>>("ListMonitorToken");
                var checkToken = _redis.GetData<string>(token + "ban");
                if(checkToken != null)
                {
                    context.Response.StatusCode = 411;
                    return;
                }

                if (listMonitorToken != null)
                {
                    foreach (var monitorToken in listMonitorToken)
                    {
                        if (monitorToken.Token.Equals(token) && monitorToken.Status == true)
                        {
                            listMonitorToken.Remove(monitorToken);
                            _redis.SetData<List<MonitorTokenModel>>("ListMonitorToken", listMonitorToken, DateTime.Now.AddYears(10));
                            _redis.SetData<string>(token + "ban", "check" , DateTime.Now.AddDays(15));
                            context.Response.StatusCode = 4111;
                            return;
                        }
                    }
                }

            }
            await _next(context);
        }
    }
}
