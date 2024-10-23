using Capstone.Api.Module.Auths.Request;
using Capstone.Application.Module.Auths.Response;
using Capstone.Domain.Module.Auth.TokenBlackList;
using MediatR;
using Microsoft.AspNetCore.Http;


namespace Capstone.Application.Module.Auth.QueryHandle
{
    public class LogoutQueryHandle : IRequestHandler<LogoutQuery, LogoutResponse>
    {
        private readonly ITokenBlacklistService _tokenBlacklistService;

        public LogoutQueryHandle(ITokenBlacklistService tokenBlacklistService)
        {
            _tokenBlacklistService = tokenBlacklistService;
        }

        public async Task<LogoutResponse> Handle(LogoutQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Token))
                {
                    return new LogoutResponse { Success = false, ErrorMessage = "Token is required." };
                }

                await _tokenBlacklistService.BlacklistTokenAsync(request.Token, StatusCodes.Status401Unauthorized);

                return new LogoutResponse { Success = true };
            }
            catch (Exception ex)
            {
                return new LogoutResponse { Success = false, ErrorMessage = "An error occurred while processing your request." + ex.Message };
            }
        }
    }
}
