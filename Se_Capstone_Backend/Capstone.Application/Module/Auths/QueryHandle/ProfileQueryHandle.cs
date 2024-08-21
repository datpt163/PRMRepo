using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auth.Response;
using Capstone.Application.Module.Auths.Query;
using Capstone.Application.Module.Auths.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.QueryHandle
{
    public class ProfileQueryHandle : IRequestHandler<ProfileQuery, ResponseMediator>
    {
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        public ProfileQueryHandle(IJwtService jwtService, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public async Task<ResponseMediator> Handle(ProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.VerifyTokenAsync(request.Token);

            if (user == null)
            {
                return new ResponseMediator("Account not found", null);
            }

            var roles = await _userManager.GetRolesAsync(user);
            var listRole = new List<RoleResponse>();
            foreach (var role in _unitOfWork.Roles.GetQuery().ToList())
                foreach (var roleInfo in roles)
                    if (roleInfo.ToUpper() == (role.Name ?? "").ToUpper())
                        listRole.Add(new RoleResponse(role.Id, role.Name ?? ""));

            var responseUser = new RegisterResponse(listRole, user.Status, user.Email ?? "", user.Id, user.UserName ?? "", user.FullName, user.PhoneNumber ?? "", user.Avatar ?? "",
                                          user.Address ?? "", user.Gender, user.Dob, user.BankAccount, user.BankAccountName,
                                          user.CreateDate, user.UpdateDate, user.DeleteDate);

            return new ResponseMediator("", responseUser);
        }
    }
}
