﻿using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Issues.Query;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Issues.QueryHandle
{
    public class GetListIssueQueryHandle : IRequestHandler<GetListIssuesQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetListIssueQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(GetListIssuesQuery request, CancellationToken cancellationToken)
        {
            if(!request.ProjectId.HasValue)
                return new ResponseMediator("User  not found", null, 404);

            var project = _unitOfWork.Projects.Find(x => x.Id == request.ProjectId).Include(c => c.Statuses).ThenInclude(c => c.Issues).ThenInclude(c => c.Assignee).FirstOrDefault();
            if(project == null)
                return new ResponseMediator("Project  not found", null, 404);

            return new ResponseMediator("", project.Statuses.SelectMany(x => x.Issues).ToList().Select(x => new
            {
                Id = x.Id,
                Title = x.Title,
                DueDate = x.DueDate,
                AssigneeId = x.Assignee.Id,
                AssigneeName = x.Assignee.UserName,
                AssigneeAvatar = x.Assignee.Avatar,
            }));
        }
    }
}