using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auths.Command;
using Capstone.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.CommandHandle
{
    public class CreateRoleCommandHandle : IRequestHandler<CreateRoleCommand, ResponseMediator>
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public CreateRoleCommandHandle(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<ResponseMediator> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RoleName))
            {
                return new ResponseMediator("Role name empty", null);
            }

            var roleExists = await _roleManager.RoleExistsAsync(request.RoleName);
            if (roleExists)
            {
                return new ResponseMediator("Role already exists", null);
            }

            var result = await _roleManager.CreateAsync(new Role() { Name = request.RoleName.ToUpper()});

            if (result.Succeeded)
            {
                var createdRole = await _roleManager.FindByNameAsync(request.RoleName);
                return new ResponseMediator("", createdRole);
            }
            else
            {
                return new ResponseMediator("Failed to create role", null);
            }
        }
    }
}
