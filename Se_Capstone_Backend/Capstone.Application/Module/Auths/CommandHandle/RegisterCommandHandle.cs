using Capstone.Application.Common.Email;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auth.Command;
using Capstone.Domain.Entities;
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
        private const string UrlConfirmEmail = "";

        public RegisterCommandHandle(UserManager<User> userManager, IEmailService emailService )
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<ResponseMediator> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingEmailAccount = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmailAccount != null)
                return new ResponseMediator("Account with this email already exists", null);

            var existingUserNameAccount = await _userManager.FindByNameAsync(request.UserName);
            if (existingUserNameAccount != null)
                return new ResponseMediator("User name is already exists", null);

            var user = new User(request.Email, request.Address, request.Gender, request.Dob, request.Phone, request.UserName, request.FullName);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var resultConfirmEmail = await _userManager.ConfirmEmailAsync(user, confirmationToken);
                return new ResponseMediator("", null);
            }
            else
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return new ResponseMediator($"User creation failed! {errors}", null);
            }
        }
    }
}
