using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auth.Response;
using Capstone.Application.Module.Auths.Query;
using Capstone.Application.Module.Auths.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;


namespace Capstone.Application.Module.Auth.QueryHandle
{
    public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, ResponseMediator>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;
        private readonly IUnitOfWork _unitOfWork;

       public RefreshTokenQueryHandler(UserManager<User> userManager, IJwtService jwtService, IOptions<JwtSettings> jwtSettings, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _jwtSettings = jwtSettings.Value;
            _unitOfWork = unitOfWork;

        }

        public async Task<ResponseMediator> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == request.RefreshToken, cancellationToken: cancellationToken);

            if (user == null)
            {
                return new ResponseMediator("Invalid refresh token", null, 400);
            }

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                // Validate and decode token
                var principal = tokenHandler.ValidateToken(request.RefreshToken, validationParameters, out SecurityToken validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;

                if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return new ResponseMediator("Invalid refresh token", null, 400);
                }

                var expiryDate = jwtToken.ValidTo;
                if (expiryDate <= DateTime.UtcNow)
                {
                    return new ResponseMediator("Refresh token has expired", null, 400);
                }

                // Tạo AccessToken mới
                var accessToken = await _jwtService.GenerateJwtTokenAsync(user, DateTime.Now.AddDays(10));
                var newRefreshToken = await _jwtService.GenerateJwtTokenAsync(user, DateTime.Now.AddDays(30));

                user.RefreshToken = newRefreshToken;
                await _userManager.UpdateAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                var role = _unitOfWork.Roles.FindOne(x => x.Name != null && x.Name == (roles.FirstOrDefault() == null ? "" : roles.FirstOrDefault()));

                return new ResponseMediator("", new LoginResponse()
                {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken,
                    User = new RegisterResponse(role == null ? null : role.Id, role == null ? null : role.Name, user.Status, user.Email ?? "", user.Id, user.UserName ?? "", user.FullName, user.PhoneNumber ?? "", user.Avatar ?? "",
                                              user.Address ?? "", user.Gender, user.Dob, user.BankAccount, user.BankAccountName,
                                              user.CreateDate, user.UpdateDate, user.DeleteDate)
                });
            }
            catch (SecurityTokenExpiredException)
            {
                return new ResponseMediator("Refresh token has expired", null, (int)HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                return new ResponseMediator($"Token validation failed: {ex.Message}", null, (int)HttpStatusCode.Unauthorized);
            }
        }
    }
}
