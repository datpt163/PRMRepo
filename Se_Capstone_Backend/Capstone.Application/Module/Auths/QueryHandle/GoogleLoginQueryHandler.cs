using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auth.Response;
using Capstone.Application.Module.Auths.Query;
using Capstone.Application.Module.Auths.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Capstone.Application.Module.Auths.QueryHandle
{
    public class GoogleLoginQueryHandler : IRequestHandler<GoogleLoginQuery, ResponseMediator>
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public GoogleLoginQueryHandler(UserManager<User> userManager, IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(GoogleLoginQuery request, CancellationToken cancellationToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
            if (payload == null)
            {
                return new ResponseMediator("Invalid Google token", null, 400);
            }

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                return new ResponseMediator("Account not found", null, 404);

            }

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = await _jwtService.GenerateJwtTokenAsync(user, DateTime.Now.AddDays(10));
            var refreshToken = await _jwtService.GenerateJwtTokenAsync(user, DateTime.Now.AddDays(30));

            var role = roles.FirstOrDefault();
            var roleEntity = _unitOfWork.Roles.Find(x => x.Name == role).FirstOrDefault();
            if(roleEntity !=null)
            {
                return new ResponseMediator("", new LoginResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    User = new RegisterResponse(roleEntity?.Id, role, user.Status, user.Email ?? "", user.Id, user.UserName ?? "", user.FullName, user.PhoneNumber ?? "", user.Avatar ?? "",
                                              user.Address ?? "", user.Gender, user.Dob, user.BankAccount, user.BankAccountName,
                                              user.CreateDate, user.UpdateDate, user.DeleteDate)
                });
            }
            return new ResponseMediator("", new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = new RegisterResponse(null, null, user.Status, user.Email ?? "", user.Id, user.UserName ?? "", user.FullName, user.PhoneNumber ?? "", user.Avatar ?? "",
                                              user.Address ?? "", user.Gender, user.Dob, user.BankAccount, user.BankAccountName,
                                              user.CreateDate, user.UpdateDate, user.DeleteDate)
            });
        }
    }

}
