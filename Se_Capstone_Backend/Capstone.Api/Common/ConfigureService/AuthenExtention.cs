using Capstone.Application.Common.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Capstone.Api.Common.ConfigureService
{
    public static class AuthenExtention
    {
        public static void AddAuthSerivce(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            string secretKey = jwtSettings["SecretKey"] ?? string.Empty;
            string issuer = jwtSettings["Issuer"] ?? string.Empty;
            string audience = jwtSettings["Audience"] ?? string.Empty;
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            }).AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection = configuration.GetSection("Authentication:Google");

                var clientId = googleAuthNSection["ClientId"];
                options.ClientId = !string.IsNullOrEmpty(clientId)
                    ? clientId
                    : Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? string.Empty;

                var clientSecret = googleAuthNSection["ClientSecret"];
                options.ClientSecret = !string.IsNullOrEmpty(clientSecret)
                    ? clientSecret
                    : Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? string.Empty;




            });
            services.Configure<JwtSettings>(jwtSettings);
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
