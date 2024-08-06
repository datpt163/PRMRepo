using Capstone.Application.Common.Jwt;
using Capstone.Infrastructure.DbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Capstone.Api.Common.ConfigureService
{
    public static class DataExtention
    {
        public static void AddDataService(this IServiceCollection services)
        {
            services.AddScoped<MyDbContext, MyDbContext>();
        }
    }
}
