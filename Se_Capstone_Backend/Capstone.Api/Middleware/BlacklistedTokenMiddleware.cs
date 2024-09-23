using Capstone.Api.Common.Constant;
using Capstone.Domain.Module.Auth.TokenBlackList;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Capstone.Api.Middleware
{
    public class BlacklistedTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenBlacklistService _tokenBlacklistService;

        public BlacklistedTokenMiddleware(RequestDelegate next, ITokenBlacklistService tokenBlacklistService)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _tokenBlacklistService = tokenBlacklistService ?? throw new ArgumentNullException(nameof(tokenBlacklistService));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Start Check Black List Token!!!");

            try
            {
                if (context.Request.Headers.TryGetValue("Authorization", out var tokenHeader) && !string.IsNullOrWhiteSpace(tokenHeader))
                {
                    Console.WriteLine("Authorization header found.");
                    var token = tokenHeader.ToString().Replace("Bearer ", "").Trim();

                    if (string.IsNullOrEmpty(token))
                    {
                        Console.WriteLine("Token is missing.");
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("Token is missing.");
                        return;
                    }

                    if (await _tokenBlacklistService.IsTokenBlacklistedAsync(token))
                    {
                        Console.WriteLine("Token is blacklisted.");
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Token is blacklisted.");
                        return;
                    }

                    if (IsTokenExpired(token, out var errorMessage))
                    {
                        Console.WriteLine("Token expired: " + errorMessage);
                        context.Response.StatusCode = Token.TokenExpired; 
                        await context.Response.WriteAsync(errorMessage);
                        return;
                    }

                    Console.WriteLine("Token is valid.");
                }
                else
                {
                    Console.WriteLine("Authorization header not found or invalid.");
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("An internal error occurred.");
            }
        }

        private bool IsTokenExpired(string token, out string errorMessage)
        {
            errorMessage = string.Empty;
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
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
            catch (Exception ex)
            {
                errorMessage = $"Invalid token: {ex.Message}";
                return true;
            }

            return false;
        }
    }
}
