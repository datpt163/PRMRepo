using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Permissions.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Permissions.CommandHandle
{
    public class CreatePermissionCommandHandle : IRequestHandler<CreatePermissionCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CreatePermissionCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ResponseMediator("Name empty", null);

            var groupPermission = await _unitOfWork.GroupPermissions.Find(s => s.Id == request.GroupPermissionId).FirstOrDefaultAsync();
            if(groupPermission == null)
                return new ResponseMediator("Group Permission not found", null);

            var permission = await _unitOfWork.Permissions.Find(p => p.Name.ToUpper().Equals(request.Name.ToUpper())).FirstOrDefaultAsync();
            if (permission != null)
                return new ResponseMediator("Permission is exist", null);

            var permissionCreated = new Permission() { Name = request.Name.ToUpper(), GroupPermissionId = request.GroupPermissionId };
            _unitOfWork.Permissions.Add(permissionCreated);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", permissionCreated);
        }
    }
}
