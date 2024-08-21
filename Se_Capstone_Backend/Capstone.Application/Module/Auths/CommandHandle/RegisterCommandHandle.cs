using Capstone.Application.Common.Email;
using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auth.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Capstone.Application.Module.Users.CommandHandle
{
    public class RegisterCommandHandle : IRequestHandler<RegisterCommand, ResponseMediator>
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandle(UserManager<User> userManager, IEmailService emailService, IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingEmailAccount = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmailAccount != null)
                return new ResponseMediator("Account with this email already exists", null);

            var existingUserNameAccount = await _userManager.FindByNameAsync(request.UserName);
            if (existingUserNameAccount != null)
                return new ResponseMediator("User name is already exists", null);

            var userCreated = await _jwtService.VerifyTokenAsync(request.Token);
            var user = new User(request.Email, request.Address, request.Gender, request.Dob, request.Phone, request.UserName, request.FullName);
            var result = await _userManager.CreateAsync(user, request.Password);
          
            if (result.Succeeded)
            {
                var createdUser = await _userManager.FindByNameAsync(request.UserName);
                if (createdUser != null)
                {
                    _unitOfWork.Staffs.Add(new Staff() { CreatedBy = createdUser.UserName ?? ""});
                    await _unitOfWork.SaveChangesAsync();
                    var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var resultConfirmEmail = await _userManager.ConfirmEmailAsync(user, confirmationToken);
                    return new ResponseMediator("", createdUser);
                }
                return new ResponseMediator($"User creation failed!", null);
            }
            else
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return new ResponseMediator($"User creation failed! {errors}", null);
            }
        }
    }
}
