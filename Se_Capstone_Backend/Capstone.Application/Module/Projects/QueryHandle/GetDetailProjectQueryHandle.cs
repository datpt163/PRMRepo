using AutoMapper;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Query;
using Capstone.Application.Module.Projects.Response;
using Capstone.Domain.Entities;
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
    public class GetDetailProjectQueryHandle : IRequestHandler<GetDetailProjectQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


      public GetDetailProjectQueryHandle(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ResponseMediator> Handle(GetDetailProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.Find(x => x.Id == request.Id).Include(c => c.Lead).Include(c => c.Users).FirstOrDefaultAsync();

            if (project == null) 
                return new ResponseMediator("Project not found", null, 404);

            var projectMapper = _mapper.Map<ProjectDetailResponse>(project);
            foreach(var p in projectMapper.Members)
            {
                User user = _unitOfWork.Users.FindOne(x => x.Id == p.Id);
                if (user != null)
                    p.RoleName = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            }

            return new ResponseMediator("", projectMapper);
        }
    }
}
