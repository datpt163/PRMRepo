using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Issues.ConsumerRabbitMq.Message;
using Capstone.Application.Module.Status.ConsumerRabbitMq.Message;
using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using Capstone.Infrastructure.Repository;
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
        private readonly IUnitOfWork _unitOfWork;
        public AddIssueConsumer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<AddIssueMessage> context)
        {
            if(context.is)
            await _unitOfWork.Issues.Add(issue);
            await _unitOfWork.SaveChangesAsync();
            var issueResponse = _unitOfWork.Issues.Find(x => x.Id == issue.Id).Include(c => c.Phase).Include(c => c.Label).Include(c => c.Status).Include(c => c.LastUpdateBy).Include(c => c.ParentIssue).Include(c => c.Reporter).Include(c => c.Assignee).Include(c => c.SubIssues).Include(c => c.Comments).FirstOrDefault();
            var response = _mapper.Map<IssueDTO?>(issueResponse);
            return new ResponseMediator("", response);
        }
    }
}
