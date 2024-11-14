﻿using AutoMapper;
using Capstone.Application.Common.FileService;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Command;
using Capstone.Application.Module.Projects.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Capstone.Application.Module.Projects.CommandHandle
{
    public class CreateProjectCommandHandle : IRequestHandler<CreateProjectCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mappper;
        private readonly IFileService _fileService;
        public CreateProjectCommandHandle(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mappper = mapper;
            _fileService = fileService;
        }
        public async Task<ResponseMediator> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _unitOfWork.Projects.Find(p => p.Code.Trim().ToUpper().Equals(request.Code.Trim().ToUpper())).FirstOrDefault();

            if (project != null)
                return new ResponseMediator("Project code is exist", null);

            //if (request.StartDate.Date < DateTime.Now.Date || request.EndDate.Date < DateTime.Now.Date)
            //    return new ResponseMediator("Start date and end date must be greater than the current time", null);

            if (request.EndDate.Date < request.StartDate.Date)
                return new ResponseMediator("End date must be greater or equal than the start date", null);
            UserDTO userDto = new UserDTO();
            if (request.LeadId != null)
            {
                var user = _unitOfWork.Users.Find(u => u.Id == request.LeadId).FirstOrDefault();
                if (user == null)
                    return new ResponseMediator("Team lead not found", null, 404);
                // var roles = await _userManager.GetRolesAsync(user);
                // if (roles == null || roles.Count == 0)
                //     return new ResponseMediator("This user not have lead project permission to become a leader", null);

                // var role = _unitOfWork.Roles.Find(x => x.Name != null && x.Name.Equals(roles.FirstOrDefault())).Include(c => c.Permissions).FirstOrDefault();
                // if (role == null)
                //     return new ResponseMediator("This user not have role to become a leader", null);

                // if (!role.Permissions.Any(x => x.Name == "IS_PROJECT_LEAD"))
                //     return new ResponseMediator("This user not have role to become a leader", null);

                userDto.Id = user.Id;
                userDto.Name = user.FullName;
            }
            var projectCreate = new Project(request.Name.Trim(), request.Code.Trim(), request.Description, request.StartDate, request.EndDate, request.LeadId, false);
            if (request.IsVisible != null)
                projectCreate.IsVisible = request.IsVisible.Value;

            _unitOfWork.Projects.Add(projectCreate);
            await _unitOfWork.SaveChangesAsync();
            var defaultStatueses = await CreateDefaultStatus(projectCreate.Id);
            var listDefaultStatus = new List<Domain.Entities.Status>();
            foreach (var s in defaultStatueses)
                listDefaultStatus.Add(new Domain.Entities.Status() { Name = s.Name, Position = s.Position, Description = s.Description, ProjectId = s.ProjectId, Color = s.Color });
            _unitOfWork.Statuses.AddRange(listDefaultStatus);
            var defaultLabels = await CreateDefaultLabels(projectCreate.Id);
            var listDefaultLabels = new List<Label>();
            foreach (var s in defaultLabels)
                listDefaultLabels.Add(new Label() { Title = s.Title, Description = s.Description, ProjectId = s.ProjectId,});
            _unitOfWork.Labels.AddRange(listDefaultLabels);
            await _unitOfWork.SaveChangesAsync();

            var response = _mappper.Map<ProjectDTO>(projectCreate);
            response.LeadId = userDto.Id;
            response.LeadName = userDto.Name;
            return new ResponseMediator("", response);
        }

        public async Task<List<Domain.Entities.Status>> CreateDefaultStatus(Guid projectId)
        {
            string module = "Module";
            string project = "Projects";
            string folder = "Default";
            string fileName = "DefaultStatus.json";
            string path = Path.Combine(module, project, folder, fileName);
            var statuses = JsonSerializer.Deserialize<List<Domain.Entities.Status>>(await _fileService.ReadFileAsync(path)) ?? new List<Domain.Entities.Status>();
            foreach (var s in statuses)
                s.ProjectId = projectId;
            return statuses;
        }

        public async Task<List<Label>> CreateDefaultLabels(Guid projectId)
        {
            string module = "Module";
            string project = "Projects";
            string folder = "Default";
            string fileName = "DefaultLabel.json";
            string path = Path.Combine(module, project, folder, fileName);
            var labels = JsonSerializer.Deserialize<List<Label>>(await _fileService.ReadFileAsync(path)) ?? new List<Label>();
            foreach (var s in labels)
                s.ProjectId = projectId;
            return labels;
        }
    }
}
