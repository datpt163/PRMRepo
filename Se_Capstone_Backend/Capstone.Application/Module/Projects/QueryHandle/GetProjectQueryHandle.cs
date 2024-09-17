using AutoMapper;
using Capstone.Application.Common.Paging;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Query;
using Capstone.Application.Module.Projects.Response;
using Capstone.Application.Module.Users.Response;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.QueryHandle
{
    public class GetProjectQueryHandle : IRequestHandler<GetListProjectQuery, PagingResultSP<ProjectDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProjectQueryHandle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagingResultSP<ProjectDTO>> Handle(GetListProjectQuery request, CancellationToken cancellationToken)
        {
            var projectsQuery = await _unitOfWork.Projects.GetQuery().Include(c => c.Lead).ThenInclude(c => c.User).ToListAsync();
            var ListProject = _mapper.Map<List<ProjectDTO>>(projectsQuery);

            if (request.IsVisible.HasValue)
                ListProject = ListProject.Where(x => x.IsVisible == request.IsVisible).ToList();

            if(request.PageIndex.HasValue && request.PageSize.HasValue)
            {
                if (request.PageIndex.Value < 1 || request.PageSize.Value < 0)
                    return new PagingResultSP<ProjectDTO>() {ErrorMessage = "PageIndex, PageSize must >= 0"};

                int skip = (request.PageIndex.Value - 1) * request.PageSize.Value;
                var projectPaing = ListProject.Skip(skip).Take(request.PageSize.Value).ToList();
                var totalCount = ListProject.Count();
                var result = new PagingResultSP<ProjectDTO>(projectPaing, totalCount, request.PageIndex.Value, request.PageSize.Value);
                return result;
            }
            return new PagingResultSP<ProjectDTO>() {Data = ListProject};
        }

    }
}
