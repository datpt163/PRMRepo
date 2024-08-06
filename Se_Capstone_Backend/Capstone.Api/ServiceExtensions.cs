using Capstone.Application.Common.Email;
using Capstone.Infrastructure.Repository;

namespace Capstone.Api
{
    public static class ServiceExtensions
    {
        public static void AddGreetingService(this IServiceCollection services)
        {
            #region ServiceCommon
            services.AddScoped<IEmailService, EmailService>();

            #endregion

            #region Middleware
            #endregion

        }
    }
}
