using AutoMapper;
using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.Paging;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Query;
using Capstone.Application.Module.Projects.Response;
using Capstone.Application.Module.Users.Response;
using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;

        public GetProjectQueryHandle(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<PagingResultSP<ProjectDTO>> Handle(GetListProjectQuery request, CancellationToken cancellationToken)
        {
            if(request.Status.HasValue)
            if (!(request.Status == ProjectStatus.NotStarted || request.Status == ProjectStatus.InProgress || request.Status == ProjectStatus.Finished))
                    return new PagingResultSP<ProjectDTO>() { ErrorMessage = "Status must more than 0 or less than 4" };

            var projectsQuery = new List<Project>();

            var user = await _jwtService.VerifyTokenAsync(request.Token);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var firstRole = roles.FirstOrDefault();
                if (firstRole != null && firstRole.Equals("ADMIN"))
                {
                    projectsQuery = await _unitOfWork.Projects.GetQuery().Include(c => c.Lead).ThenInclude(c => c.User).ToListAsync();
                }
                else
                {
                    if(user.Staff !=  null)
                    {
                        projectsQuery = user.Staff.Projects.ToList();
                        projectsQuery.AddRange(user.Staff.LeadProjects.ToList());
                    }
                }
            }
          
            var ListProject = _mapper.Map<List<ProjectDTO>>(projectsQuery);

            if (request.IsVisible.HasValue)
                ListProject = ListProject.Where(x => x.IsVisible == request.IsVisible).ToList();
            if(request.Status.HasValue)
                ListProject = ListProject.Where(x => x.Status == request.Status).ToList();

            if (request.PageIndex.HasValue && request.PageSize.HasValue)
            {
                if (request.PageIndex.Value < 1 || request.PageSize.Value < 0)
                    return new PagingResultSP<ProjectDTO>() {ErrorMessage = "PageIndex, PageSize must >= 0"};

                int skip = (request.PageIndex.Value - 1) * request.PageSize.Value;
                var projectPaing = ListProject.OrderByDescending(c => c.CreatedAt).Skip(skip).Take(request.PageSize.Value).ToList();
                var totalCount = ListProject.Count();
                var result = new PagingResultSP<ProjectDTO>(projectPaing, totalCount, request.PageIndex.Value, request.PageSize.Value);
                return result;
            }
            return new PagingResultSP<ProjectDTO>() {Data = ListProject.OrderByDescending(c => c.CreatedAt).ToList()};
        }
    }
}
