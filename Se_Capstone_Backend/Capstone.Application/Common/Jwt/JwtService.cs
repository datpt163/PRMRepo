using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContext;
using Microsoft.IdentityModel.Tokens;
namespace Capstone.Application.Common.Jwt
{
    public class JwtService : IJwtService
    {
        private const string secretKey = "ijurkbdlhmklqacwqzdxmkkhvqowlyqa99";
        private const string issuer = "localhost:7144";

        public async Task<string> generatejwttokentw(User account)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id + ""),
                //new Claim(ClaimTypes.Role , account.Role.Name + "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtsecuritytoken = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtsecuritytoken);
        }

        public async Task<User> VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                // Validate token and get principal
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Get claims from principal
                var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                //var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                if (userIdClaim == null)
                {
                    throw new SecurityTokenException("Invalid token");
                }

                var userId = Guid.Parse(userIdClaim.Value);
                var acc = MyDbContext.Users.FirstOrDefault(s => s.Id == userId);

                if (acc == null)
                    return null;
                return acc;
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Token validation failed", ex);
            }
        }
    }
}
