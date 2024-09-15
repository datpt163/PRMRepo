using AutoMapper;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Command;
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

namespace Capstone.Application.Module.Projects.CommandHandle
{
    public class CreateProjectCommandHandle : IRequestHandler<CreateProjectCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mappper;

        public CreateProjectCommandHandle(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mappper = mapper;
        }
        public async Task<ResponseMediator> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _unitOfWork.Projects.Find(p => p.Code.Trim().ToUpper().Equals(request.Code.Trim().ToUpper())).FirstOrDefault();

            if (project != null)
                return new ResponseMediator("Project code is exist", null);

            if (request.StartDate < DateTime.Now || request.EndDate < DateTime.Now)
                return new ResponseMediator("Start date and end date must be greater than the current time", null);

            if (request.EndDate <= request.StartDate)
                return new ResponseMediator("End date must be greater than the start date", null);
            UserDTO userDto = new UserDTO();
            if (request.TeamLeadId != null)
            {
                var user = _unitOfWork.Users.Find(u => u.Id == request.TeamLeadId).FirstOrDefault();
                if (user == null)
                    return new ResponseMediator("Team lead not found", null,404);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles == null || roles.Count == 0)
                    return new ResponseMediator("This user not have role to become a leader", null);

                var role = _unitOfWork.Roles.Find(x => x.Name != null && x.Name.Equals(roles.FirstOrDefault())).Include(c => c.Permissions).FirstOrDefault();
                if ( role == null)
                    return new ResponseMediator("This user not have role to become a leader", null);
                
                if (!role.Permissions.Any(x => x.Name == "IS_PROJECT_LEAD"))
                    return new ResponseMediator("This user not have role to become a leader", null);

                userDto.Id = user.Id;
                userDto.Name = user.FullName;
            }
            var projectCreate = new Project(request.Name.Trim(), request.Code.Trim(), request.Description, request.StartDate, request.EndDate, request.TeamLeadId, false);
          
            _unitOfWork.Projects.Add(projectCreate);
            await _unitOfWork.SaveChangesAsync();
            var response =  _mappper.Map<ProjectDTO>(projectCreate);
            response.LeadId = userDto.Id;
            response.LeadName = userDto.Name;
            return new ResponseMediator("", response);
        }
    }
}
