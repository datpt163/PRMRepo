using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auths.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Redis;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace Capstone.Application.Module.Auths.CommandHandle
{
    public class ResetPasswordCommandHandle : IRequestHandler<ResetPasswordCommand, ResponseMediator>
    {
        private readonly IJwtService _jwtService;
        private readonly RedisContext _redisContext;
        private readonly UserManager<User> _userManager;

        public ResetPasswordCommandHandle(IJwtService jwtService, RedisContext redisContext, UserManager<User> userManager)
        {
            _jwtService = jwtService;
            _redisContext = redisContext;
            _userManager = userManager;
        }

        public async Task<ResponseMediator> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.NewPassword))
                return new ResponseMediator("Password is null or empty", null);

            if (string.IsNullOrEmpty(request.Code))
                return new ResponseMediator("Code is null or empty", null);

            var blackList = _redisContext.GetData<List<string>>("BlackListForgotPasswordCode");

            if (blackList != null)
                if (blackList.Contains(request.Code))
                    return new ResponseMediator("This code has already been used", null);

            try
            {
                var ac = await _jwtService.VerifyTokenAsync(request.Code, "da-32-character-ultra-secure-and-ultra-long-secret");
               
                if(ac != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(ac);
                    var result = await _userManager.ResetPasswordAsync(ac, token, request.NewPassword);
                    if (!result.Succeeded)
                    {
                        var errors = result.Errors.Select(error => error.Description).ToList();
                        var errorMessage = string.Join(", ", errors);
                        return new ResponseMediator(errorMessage, null);
                    }

                    if (blackList != null)
                    {
                        blackList.Add(request.Code);
                        _redisContext.SetData<List<string>>("BlackListForgotPasswordCode", blackList, DateTime.Now.AddYears(1000));
                    }

                    _redisContext.SetData<List<string>>("BlackListForgotPasswordCode", new List<string>() { request.Code }, DateTime.Now.AddYears(1000));
                    return new ResponseMediator("", null);
                }
                return new ResponseMediator("Some thing wrong", null);
            }
            catch
            {
                return new ResponseMediator("Code reset password was expired or incorrect", null);
            }
        }
    }
}
