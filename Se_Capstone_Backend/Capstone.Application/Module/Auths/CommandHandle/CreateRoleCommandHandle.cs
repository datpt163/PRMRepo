﻿using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auths.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandle(RoleManager<Role> roleManager, IUnitOfWork unitOfWork)
        {
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseMediator> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return new ResponseMediator("Role name empty", null);
            }

            if (string.IsNullOrEmpty(request.Description))
            {
                return new ResponseMediator("Description empty", null);
            }

            var roleExists = await _roleManager.RoleExistsAsync(request.Name);
            if (roleExists)
            {
                return new ResponseMediator("Role already exists", null);
            }

            var result = await _roleManager.CreateAsync(new Role() { Name = request.Name.ToUpper()});

            if (result.Succeeded)
            {
                var createdRole = await _unitOfWork.Roles.Find(c => c.Name == request.Name.ToUpper()).Include(c => c.Permissions).FirstOrDefaultAsync();
                if (createdRole != null) {
                    createdRole.Description = request.Description;
                    foreach (var p in request.PermissionsId) {
                        var permission = await _unitOfWork.Permissions.FindOneAsync(c => c.Id == p);
                        if (permission != null) 
                            createdRole.Permissions.Add(permission);
                    }
                    _unitOfWork.Roles.Update(createdRole);
                    await _unitOfWork.SaveChangesAsync();
                    return new ResponseMediator("", new { id = createdRole.Id, name = createdRole.Name, description = createdRole.Description, permissions = createdRole.Permissions });
                }
                return new ResponseMediator("Failed to create role", null);
            }
            else
            {
                return new ResponseMediator("Failed to create role", null);
            }
        }
    }
}
