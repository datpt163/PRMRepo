using Capstone.Application.Common.Jwt;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContexts;
using Capstone.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Capstone.Api.Common.ConfigureService
{
    public static class DataExtention
    {
        public static void AddDataService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SeCapstoneContext>(options => options.UseNpgsql(configuration.GetConnectionString("value")));
            services.AddScoped<MyDbContext, MyDbContext>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
