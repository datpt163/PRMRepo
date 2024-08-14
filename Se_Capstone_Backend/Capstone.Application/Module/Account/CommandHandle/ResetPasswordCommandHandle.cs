using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Account.Command;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Account.CommandHandle
{
    public class ResetPasswordCommandHandle : IRequestHandler<ResetPasswordCommand, ResponseMediator>
    {
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ResetPasswordCommandHandle> _logger;

        public ResetPasswordCommandHandle(IJwtService jwtService, IUnitOfWork unitOfWork, ILogger<ResetPasswordCommandHandle> logger)
        {
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ResponseMediator> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _jwtService.VerifyToken(request.Code);
                if (account == null)
                    return new ResponseMediator("Account not found", null);

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                account.Password = hashedPassword;
                _unitOfWork.Users.Update(account);
                await _unitOfWork.SaveChangesAsync();
                return new ResponseMediator("", null);
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogInformation(ex.Message);
                return new ResponseMediator("Code is expire", null);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogInformation(ex.Message);
                return new ResponseMediator("Code is not valid", null);
            }
        }
    }
}
