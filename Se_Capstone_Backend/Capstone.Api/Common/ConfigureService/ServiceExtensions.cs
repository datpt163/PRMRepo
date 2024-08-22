using Capstone.Application.Common.Email;
using Capstone.Domain.Module.Auth.TokenBlackList;
using Capstone.Infrastructure.Redis;
using Capstone.Infrastructure.Repository;

namespace Capstone.Api.Common.ConfigureService
{
    public static class ServiceExtensions
    {
        public static void AddGreetingService(this IServiceCollection services, IConfiguration configuration)
        {
            #region ServiceCommon
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<RedisSettings>(configuration.GetSection("RedisDBSettings"));
            services.AddSingleton<RedisContext, RedisContext>();
            services.AddSingleton<ITokenBlacklistService, TokenBlacklistService>();

            #endregion

            #region Middleware
            #endregion

        }
    }
}
