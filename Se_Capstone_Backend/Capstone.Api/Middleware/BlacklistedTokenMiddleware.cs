using Capstone.Api.Common.Constant;
using Capstone.Domain.Module.Auth.TokenBlackList;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Capstone.Api.Middleware
{
    public class BlacklistedTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenBlacklistService _tokenBlacklistService;
        private readonly IConfiguration _configuration;

        public BlacklistedTokenMiddleware(RequestDelegate next, ITokenBlacklistService tokenBlacklistService, IConfiguration configuration)
        {
            _next = next;
            _tokenBlacklistService = tokenBlacklistService;
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Start Check Black List Token!!!");

            try
            {
                if (context.Request.Headers.TryGetValue("Authorization", out var tokenHeader))
                {
                    Console.WriteLine("In the fun check token!!!");
                    var token = tokenHeader.ToString().Replace("Bearer ", "").Trim();

                    var authorizeCode = await _tokenBlacklistService.IsTokenBlacklistedAsync(token);

                    if (authorizeCode != null)
                    {
                        context.Response.StatusCode = Token.TokenLogout;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync("{\"status\": 403, \"message\": \"Token is blacklisted.\"}");
                        return;
                    }

                    if (IsTokenExpired(token, out var errorMessage))
                    {
                        Console.WriteLine("Token expired!!!");

                        context.Response.StatusCode = Token.TokenExpired;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync($"{{\"message\": \"{errorMessage}\"}}");
                        return;
                    }
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"message\": \"An internal error occurred.\"}");
            }
        }

        private bool IsTokenExpired(string token, out string errorMessage)
        {
            errorMessage = string.Empty;
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSettings = _configuration.GetSection("JwtSettings");
            string secretKey = jwtSettings["SecretKey"] ?? string.Empty;
            string issuer = jwtSettings["Issuer"] ?? string.Empty;
            string audience = jwtSettings["Audience"] ?? string.Empty;

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), 
                    ValidateIssuer = true,
                    ValidIssuer = issuer,  
                    ValidateAudience = true,
                    ValidAudience = audience,  
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtToken && jwtToken.ValidTo < DateTime.UtcNow)
                {
                    errorMessage = "Token has expired.";
                    return true;
                }
            }
            catch (SecurityTokenExpiredException)
            {
                errorMessage = "Token has expired.";
                return true;
            }
            catch (SecurityTokenValidationException ex)
            {
                errorMessage = $"Invalid token: {ex.Message}";
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"An unexpected error occurred: {ex.Message}";
                return true;
            }

            return false;
        }
    }
}
