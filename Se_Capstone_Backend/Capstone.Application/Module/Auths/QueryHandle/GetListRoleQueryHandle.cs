using AutoMapper;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auths.Query;
using Capstone.Application.Module.Auths.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


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
            IQueryable<Role> rolesQuery = _unitOfWork.Roles.GetQuery()
                .Include(p => p.Permissions);

            if (!string.IsNullOrEmpty(request.Name))
            {
                rolesQuery = rolesQuery.Where(r => r.Name != null && r.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var roles = await rolesQuery.ToListAsync(cancellationToken);
            return new ResponseMediator("", _mapper.Map<List<RoleDTO>>(roles));
        }


    }
}
