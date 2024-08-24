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
    public class CreateGroupPermissionCommandHandle : IRequestHandler<CreateGroupPermissionCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateGroupPermissionCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(CreateGroupPermissionCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.Name))
                return new ResponseMediator("Name empty", null);

            var groupPermission = await _unitOfWork.GroupPermissions.Find(p => p.Name.ToUpper().Equals(request.Name.ToUpper())).FirstOrDefaultAsync();
            if (groupPermission != null) 
                return new ResponseMediator("Group permission is exist", null);

            var groupPermissionCreated = new GroupPermission() { Name = request.Name.ToUpper() };
                _unitOfWork.GroupPermissions.Add(groupPermissionCreated);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", groupPermissionCreated);
        }
    }
}
