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
    public class GetPermissionQueryHandle : IRequestHandler<GetPermissionQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPermissionQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async  Task<ResponseMediator> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            return new ResponseMediator("", await _unitOfWork.Permissions.GetQuery().Include(c => c.GroupPermission).ToListAsync());
        }
    }
}
