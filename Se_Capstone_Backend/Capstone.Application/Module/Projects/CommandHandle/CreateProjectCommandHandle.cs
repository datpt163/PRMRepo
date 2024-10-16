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

            if (request.StartDate.Date < DateTime.Now.Date || request.EndDate.Date < DateTime.Now.Date)
                return new ResponseMediator("Start date and end date must be greater than the current time", null);

            if (request.EndDate.Date < request.StartDate.Date)
                return new ResponseMediator("End date must be greater or equal than the start date", null);
            UserDTO userDto = new UserDTO();
            if (request.LeadId != null)
            {
                var user = _unitOfWork.Users.Find(u => u.Id == request.LeadId).FirstOrDefault();
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
            var projectCreate = new Project(request.Name.Trim(), request.Code.Trim(), request.Description, request.StartDate, request.EndDate, request.LeadId, false);
            if (request.IsVisible != null)
                projectCreate.IsVisible = request.IsVisible.Value;

            _unitOfWork.Projects.Add(projectCreate);
            await _unitOfWork.SaveChangesAsync();
            _unitOfWork.Statuses.AddRange(CreateDefaultStatus(projectCreate.Id));
            await _unitOfWork.SaveChangesAsync();

            var response =  _mappper.Map<ProjectDTO>(projectCreate);
            response.LeadId = userDto.Id;
            response.LeadName = userDto.Name;
            return new ResponseMediator("", response);
        }

        public List<Domain.Entities.Status> CreateDefaultStatus(Guid projectId)
        {
            return new List<Domain.Entities.Status>() { 
                new Domain.Entities.Status() {Name = "Backlog", Color = "blackAlpha", Description = "Plan to do in this phase", Position = 1, ProjectId = projectId} ,
                new Domain.Entities.Status() {Name = "Todo", Color = "telegram", Description = "Plan to do in this week", Position = 2, ProjectId = projectId},
                new Domain.Entities.Status() {Name = "In progress", Color = "telegram", Description = "Tasks that you are working on", Position = 3, ProjectId = projectId},
                new Domain.Entities.Status() {Name = "In review", Color = "telegram", Description = "This task is waiting for review", Position = 4, ProjectId = projectId},
                new Domain.Entities.Status() {Name = "Done", Color = "green", Description = "This task is completed", Position = 5, ProjectId = projectId},
                new Domain.Entities.Status() {Name = "Cancelled", Color = "yellow", Description = "Tasks that are decided not to execute anymore", Position = 6, ProjectId = projectId},
            };
        }
    }
}
