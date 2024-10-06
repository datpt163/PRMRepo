using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Capstone.Application.Common.Email;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContexts;
using Capstone.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace Capstone.Application.Common.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public JwtService(IOptions<JwtSettings> jwtSettings, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<string> GenerateJwtTokenAsync(User account, DateTime expireTime, string secretKeyReserve = "")
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id + ""),
            };

            var roles = await _userManager.GetRolesAsync(account);
            var permissions = _unitOfWork.Permissions.GetQuery()
                                         .Where(p => p.Roles.Any(r => roles.Contains(r.Name ?? ""))) .ToList();

            foreach (var permission in permissions)
            {
                claims.Add(new Claim(ClaimTypes.Role, permission.Name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            
            if(!string.IsNullOrEmpty(secretKeyReserve))
                key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyReserve));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtsecuritytoken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expireTime,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtsecuritytoken);
        }

        public async Task<User?> VerifyTokenAsync(string token, string secretKeyReserve = "")
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyJwt = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            if (!string.IsNullOrEmpty(secretKeyReserve))
                keyJwt = Encoding.UTF8.GetBytes(secretKeyReserve);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(keyJwt),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                {
                    throw new SecurityTokenException("Invalid token");
                }
                var userId = Guid.Parse(userIdClaim.Value);
                var account = await _unitOfWork.Users.Find(s => s.Id == userId).Include(c => c.Projects).ThenInclude(c => c.Lead).Include(c => c.LeadProjects).FirstOrDefaultAsync();

                return account;
            }
            catch (SecurityTokenExpiredException ex)
            {
                throw new SecurityTokenExpiredException("Token has expired.", ex);
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Token verification failed.", ex);
            }
        }
    }
}
