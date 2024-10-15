using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Status.Command;
using Capstone.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Status.CommandHandle
{
    public class UpdateStatusCommandHandle : IRequestHandler<UpdateStatusCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStatusCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            var status = _unitOfWork.Statuses.Find(x => x.Id == request.Id).FirstOrDefault();
            if (status == null)
                return new ResponseMediator("Status not found", null, 404);
            if (string.IsNullOrEmpty(request.Name))
                return new ResponseMediator("Name empty", null, 400);

            if (string.IsNullOrEmpty(request.Color))
                return new ResponseMediator("Color empty", null, 400);

            var statusCheckDuplicateTitle = _unitOfWork.Statuses.FindOne(x => x.Id != request.Id && x.ProjectId == status.ProjectId && x.Name.Trim().ToUpper() == request.Name.Trim().ToUpper());
            if (statusCheckDuplicateTitle != null)
                return new ResponseMediator("This name status is availble", null, 400);

            status.Name = request.Name;
            status.Description = request.Description;
            status.Color = request.Color;
            _unitOfWork.Statuses.Update(status);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", status);
        }
    }
}
