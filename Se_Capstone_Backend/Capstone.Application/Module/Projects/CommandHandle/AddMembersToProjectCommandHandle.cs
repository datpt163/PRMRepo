﻿using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Application.Module.Projects.CommandHandle
{
    public class AddMembersToProjectCommandHandle : IRequestHandler<AddMembersToProject, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddMembersToProjectCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(AddMembersToProject request, CancellationToken cancellationToken)
        {
            var project = _unitOfWork.Projects.Find(x => x.Id == request.ProjectId).Include(c => c.UserProjects).FirstOrDefault();
            if (project == null)
                return new ResponseMediator("Project not found", null, 404);
            if(request.MemberIds.Count() == 0)
                return new ResponseMediator("List member empty", null, 400);

            project.UserProjects = new List<UserProject>();
            foreach (var s in request.MemberIds)
            {
                var staff = _unitOfWork.Users.FindOne(x => x.Id == s);
                if (staff == null)
                    return new ResponseMediator("Member not found", null, 404);
                if(staff.Id != project.LeadId)
                    project.UserProjects.Add(new UserProject() { ProjectId = request.ProjectId, UserId = staff.Id, IsIssueConfigurator = false, IsProjectConfigurator = false});
            }
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", null);
        }
    }
}
