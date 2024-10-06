using Capstone.Application.Common.Email;
using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auth.Command;
using Capstone.Application.Module.Auths.Response;
using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using Capstone.Infrastructure.Repository;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet.Core;
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
        private readonly RoleManager<Capstone.Domain.Entities.Role> _roleManager;

        public RegisterCommandHandle(UserManager<User> userManager, IEmailService emailService, IJwtService jwtService, IUnitOfWork unitOfWork, RoleManager<Domain.Entities.Role> roleManager)
        {
            _userManager = userManager;
            _emailService = emailService;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager; 
        }

        public async Task<ResponseMediator> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var role = _unitOfWork.Roles.Find(x => x.Id == request.RoleId).FirstOrDefault();
            if (role == null)
                return new ResponseMediator("Role not found", null, 404);

            var existingEmailAccount = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmailAccount != null)
                return new ResponseMediator("Account with this email already exists", null, 400);

            var existingUserNameAccount = await _userManager.FindByNameAsync(request.UserName);
            if (existingUserNameAccount != null)
                return new ResponseMediator("User name is already exists", null, 400);

            var user = new User(request.Email, request.Address, request.Gender, request.Dob, request.Phone, request.UserName, request.FullName);
            var result = await _userManager.CreateAsync(user, request.Password);
          
            if (result.Succeeded)
            {
                var createdUser = await _userManager.FindByNameAsync(request.UserName);
                if (createdUser != null)
                {
                    await _unitOfWork.SaveChangesAsync();
                    var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var resultConfirmEmail = await _userManager.ConfirmEmailAsync(user, confirmationToken);
                    var responseSave = await _userManager.FindByNameAsync(request.UserName);
                    var responseUser = new CreateUserResponse(role.Id, role.Name ?? "", responseSave.Status , responseSave.Email ?? "", responseSave.Id, responseSave.UserName ?? "", responseSave.FullName,  responseSave.PhoneNumber ?? "", responseSave.Avatar ?? "",
                                             responseSave.Address ?? "" , responseSave.Gender, responseSave.Dob, responseSave.BankAccount, responseSave.BankAccountName,
                                             responseSave.CreateDate, responseSave.UpdateDate, responseSave.DeleteDate);;

                    await _userManager.AddToRoleAsync(user, role.Name ?? "");
                    return new ResponseMediator("", responseUser);
                }
                return new ResponseMediator($"User creation failed!", null, 400);
            }
            else
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return new ResponseMediator($"User creation failed! {errors}", null);
            }
        }
    }
}
