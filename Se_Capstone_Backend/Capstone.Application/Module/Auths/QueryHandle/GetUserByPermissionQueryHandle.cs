using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auths.Query;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.QueryHandle
{
    public class GetUserByPermissionQueryHandle : IRequestHandler<GetUserByPermissionQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserByPermissionQueryHandle (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseMediator> Handle(GetUserByPermissionQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.permissionName))
                return new ResponseMediator("Permission name empty", null, 400);

            var permission = await _unitOfWork.Permissions.Find(x => x.Name.Trim().ToUpper().Equals(request.permissionName.Trim().ToUpper())).Include(c => c.Roles).ThenInclude(c => c.Users).FirstOrDefaultAsync();

            if (permission == null)
                return new ResponseMediator("Permission not found", null, 404 );

            var users = permission.Roles
                     .SelectMany(role => role.Users)
                     .Select(x => new
                     {
                         Id = x.Id,
                         UserName = x.UserName,
                         FullName = x.FullName,
                         Dob = x.Dob,
                         Email = x.Email,
                     })
                     .ToList();
            return new ResponseMediator("", users);
        }
    }
}
