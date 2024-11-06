using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Issues.ConsumerRabbitMq.Message;
using Capstone.Application.Module.Status.ConsumerRabbitMq.Message;
using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Issues.ConsumerRabbitMq
{
    public class AddIssueConsumer : IConsumer<AddIssueMessage>
    {
        public async Task Consume(ConsumeContext<AddIssueMessage> context)
        {
            //if (request.ParentIssueId.HasValue)
            //    if (_unitOfWork.Issues.FindOne(x => x.Id == request.ParentIssueId.Value) == null)
            //        return new ResponseMediator("Parent issue not found", null, 404);
            //if (request.AssignedToId.HasValue && _unitOfWork.Users.FindOne(x => x.Id == request.AssignedToId) == null)
            //    return new ResponseMediator("Assigned user not found", null, 404);

            //if (request.LabelId.HasValue && _unitOfWork.Labels.FindOne(x => x.Id == request.LabelId) == null)
            //    return new ResponseMediator("Label  not found", null, 404);

            //var status = _unitOfWork.Statuses.Find(x => x.Id == request.StatusId).Include(c => c.Project).ThenInclude(c => c.Phases).Include(c => c.Issues).FirstOrDefault();
            //if (status == null)
            //    return new ResponseMediator("Status  not found", null, 404);

            //var user = await _jwtService.VerifyTokenAsync(request.Token);
            //if (user == null)
            //    return new ResponseMediator("User  not found", null, 404);

            //await Task.Delay(TimeSpan.FromSeconds(20), cancellationToken);

            //var lastUpdateById = user.Id;
            //var assignedById = user.Id;
            //var index = SetIndex(status.Project.Id);
            //var position = status.Issues.Count() + 1;

            //Guid? phaseId = null;
            //var result = status.Project.GetStatusPhaseOfProject();
            //if ((result.status == PhaseStatus.Running || result.status == PhaseStatus.Complete) && result.phaseRunning != null)
            //    phaseId = result.phaseRunning.Id;

            //var issue = new Issue()
            //{
            //    Index = index,
            //    Title = request.Title,
            //    Description = request.Description,
            //    StartDate = request.StartDate,
            //    DueDate = request.DueDate,
            //    Priority = request.Priority,
            //    EstimatedTime = request.EstimatedTime,
            //    Position = position,
            //    ParentIssueId = request.ParentIssueId,
            //    ReporterId = assignedById,
            //    AssigneeId = request.AssignedToId,
            //    LastUpdateById = lastUpdateById,
            //    StatusId = request.StatusId,
            //    LabelId = request.LabelId,
            //    PhaseId = phaseId
            //};
            //_unitOfWork.Issues.Add(issue);
            //await _unitOfWork.SaveChangesAsync();
            //var issueResponse = _unitOfWork.Issues.Find(x => x.Id == issue.Id).Include(c => c.Phase).Include(c => c.Label).Include(c => c.Status).Include(c => c.LastUpdateBy).Include(c => c.ParentIssue).Include(c => c.Reporter).Include(c => c.Assignee).Include(c => c.SubIssues).Include(c => c.Comments).FirstOrDefault();
            //var response = _mapper.Map<IssueDTO?>(issueResponse);
            //return new ResponseMediator("", response);
        }
    }
}
