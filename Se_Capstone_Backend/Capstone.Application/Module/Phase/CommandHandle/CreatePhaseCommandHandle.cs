using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Phase.Command;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Phase.CommandHandle
{
    public class CreatePhaseCommandHandle : IRequestHandler<CreatePhaseCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatePhaseCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(CreatePhaseCommand request, CancellationToken cancellationToken)
        {
            var project = _unitOfWork.Projects.Find(x => x.Id == request.ProjectId).Include(x => x.Phases).FirstOrDefault();

            if (project == null)
                return new ResponseMediator("Project not found", null, 404);

            if (string.IsNullOrEmpty(request.Title))
                return new ResponseMediator("Title empty", null, 404);
            return new ResponseMediator("Title empty", null, 404);
            //if (request.StartDate.Date < DateTime.Now.Date || request.EndDate.Date < DateTime.Now.Date)
            //    return new ResponseMediator("Start date and end date must be greater than the current time", null);

            //if (request.EndDate.Date < request.StartDate.Date)
            //    return new ResponseMediator("End date must be greater or equal than the start date", null);

        }
    }
}
