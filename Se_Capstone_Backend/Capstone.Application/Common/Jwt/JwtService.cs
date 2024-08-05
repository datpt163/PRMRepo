using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Capstone.Application.Common.Email;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContext;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace Capstone.Application.Common.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> generatejwttokentw(User account, int expireTime = 30)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id + ""),
            };
            // add role into claim
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtsecuritytoken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(expireTime),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtsecuritytoken);
        }

        public async Task<User> VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                 ClockSkew = TimeSpan.Zero
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
            catch 
            {
                return null;
            }
        }
    }
}
