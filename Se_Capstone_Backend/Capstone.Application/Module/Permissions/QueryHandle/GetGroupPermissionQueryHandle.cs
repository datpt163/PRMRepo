using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Permissions.Query;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Permissions.QueryHandle
{
    public class GetGroupPermissionQueryHandle : IRequestHandler<GetGroupPermissionQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGroupPermissionQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(GetGroupPermissionQuery request, CancellationToken cancellationToken)
        {
            var groups = await _unitOfWork.GroupPermissions.GetQuery().Include(c => c.Permissions).ToListAsync();
            return new ResponseMediator("", groups);
        }
    }
}
