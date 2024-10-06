using AutoMapper;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Command;
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

namespace Capstone.Application.Module.Projects.CommandHandle
{
    public class UpdateProjectCommandHandle : IRequestHandler<UpdateProjectCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public UpdateProjectCommandHandle(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager; 
        }

        public async Task<ResponseMediator> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            if( !(request.Status == ProjectStatus.NotStarted || request.Status == ProjectStatus.InProgress || request.Status == ProjectStatus.Finished ))
                return new ResponseMediator("Status must more than 0 or less than 4", null);

            var projectCheckCode = _unitOfWork.Projects.Find(p => p.Code.Trim().ToUpper().Equals(request.Code.Trim().ToUpper()) && request.Id != p.Id).FirstOrDefault();

            if (projectCheckCode != null)
                return new ResponseMediator("Project code is exist", null);

            if (request.EndDate.Date < request.StartDate.Date)
                return new ResponseMediator("End date must be greater or equal than the start date", null);
           

            var project = _unitOfWork.Projects.Find(x => x.Id == request.Id).Include(c => c.Lead).FirstOrDefault();
            if (project == null)
                return new ResponseMediator("Project not found", null, 404);

            if (request.TeamLeadId != null)
            {
                var user = _unitOfWork.Users.Find(u => u.Id == request.TeamLeadId).FirstOrDefault();
                if (user == null)
                    return new ResponseMediator("Team lead not found", null, 404);
                var roles = await _userManager.GetRolesAsync(user);
                if (roles == null || roles.Count == 0)
                    return new ResponseMediator("This user not have role to become a leader", null);

                var role = _unitOfWork.Roles.Find(x => x.Name != null && x.Name.Equals(roles.FirstOrDefault())).Include(c => c.Permissions).FirstOrDefault();
                if (role == null)
                    return new ResponseMediator("This user not have role to become a leader", null);

                if (!role.Permissions.Any(x => x.Name == "IS_PROJECT_LEAD"))
                    return new ResponseMediator("This user not have role to become a leader", null);
            }

            project.Name = request.Name.Trim();
            project.Code = request.Code.Trim();
            project.Description = request.Description;
            project.StartDate = request.StartDate;
            project.EndDate = request.EndDate;
            project.LeadId = request.TeamLeadId;
            project.UpdatedAt = DateTime.Now;
            project.Status = request.Status;
            project.LeadId = request.TeamLeadId;
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();
            var response = _mapper.Map<ProjectDTO>(project);
            return new ResponseMediator("", response);
        }
    }
}
