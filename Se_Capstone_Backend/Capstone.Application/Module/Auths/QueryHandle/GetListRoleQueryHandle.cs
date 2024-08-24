using AutoMapper;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auths.Query;
using Capstone.Application.Module.Auths.Response;
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
    public class GetListRoleQueryHandle : IRequestHandler<GetListRoleQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetListRoleQueryHandle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseMediator> Handle(GetListRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.Roles.GetQuery().Include(p => p.Permissions).ToListAsync();
            return new ResponseMediator("", _mapper.Map<List<RoleDTO>>(roles));
        }
    }
}
