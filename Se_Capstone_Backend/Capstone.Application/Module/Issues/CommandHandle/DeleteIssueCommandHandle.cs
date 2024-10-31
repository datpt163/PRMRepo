using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Issues.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Issues.CommandHandle
{
    public class DeleteIssueCommandHandle : IRequestHandler<DeleteIssueCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteIssueCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(DeleteIssueCommand request, CancellationToken cancellationToken)
        {
           var issue = _unitOfWork.Issues.Find(x => x.Id == request.Id).Include(c => c.Status).ThenInclude(d => d.Issues).FirstOrDefault();
           if (issue == null)
                return new ResponseMediator("Issue not found", null, 404);

            foreach (var iss in issue.Status.Issues)
                if (iss.Position > issue.Position)
                    iss.Position--;

            _unitOfWork.Issues.Update(issue);
            _unitOfWork.Issues.Remove(issue);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", null);
        }
    }
}
