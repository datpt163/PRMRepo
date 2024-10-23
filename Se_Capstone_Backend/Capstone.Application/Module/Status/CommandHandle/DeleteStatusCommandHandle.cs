using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Status.Command;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Application.Module.Status.CommandHandle
{
    public class DeleteStatusCommandHandle : IRequestHandler<DeleteStatusCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteStatusCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseMediator> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
        {
            var status = _unitOfWork.Statuses.Find(x => x.Id == request.Id).Include(c => c.Issues).Include(c => c.Project).ThenInclude(c => c.Statuses).FirstOrDefault();
            if (status == null)
                return new ResponseMediator("Status not found", null, 404);
            if (status.Issues.Count != 0)
            {
                if (!request.NewStatusId.HasValue)
                    return new ResponseMediator("New status null", null, 400);
                var newStatus = _unitOfWork.Statuses.Find(x => x.Id == request.NewStatusId).FirstOrDefault();
                if (newStatus == null)
                    return new ResponseMediator("New status not found", null, 404);
                if (newStatus.ProjectId != status.ProjectId)
                    return new ResponseMediator("Old status and new status not same project", null, 400);

                foreach (var i in status.Issues)
                    i.StatusId = request.NewStatusId.Value;
            }
            foreach (var stat in status.Project.Statuses)
            {
                if(stat.Position > status.Position) 
                    stat.Position = stat.Position - 1;
            }
            _unitOfWork.Statuses.Update(status);
            _unitOfWork.Statuses.Remove(status);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", null);
        }

    }
}
