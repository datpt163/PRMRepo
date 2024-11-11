using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Phase.Command;
using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Capstone.Application.Module.Phase.CommandHandle
{
    public class UpdatePhaseCommandHandle : IRequestHandler<UpdatePhaseCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePhaseCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(UpdatePhaseCommand request, CancellationToken cancellationToken)
        {
            var phase = _unitOfWork.Phases.Find(x => x.Id == request.Id).Include(c => c.Project).ThenInclude(p => p.Phases).FirstOrDefault();
            var p = phase;
            if(phase == null)
                return new ResponseMediator("Phase not found", null, 404);
            if(phase.ActualEndDate.HasValue)
                return new ResponseMediator("This phase was done", null);

            var phaseCheckDuplicateTitle = _unitOfWork.Phases.FindOne(x => x.Id != request.Id && x.ProjectId == phase.ProjectId && x.Title.Trim().ToUpper() == request.Title.Trim().ToUpper());
            if(phaseCheckDuplicateTitle != null)
                return new ResponseMediator("This title is availble", null, 400);

            //if (request.ExpectedStartDate.Date < phase.Project.StartDate)
            //    return new ResponseMediator("Start date must be greater or equal than the start date of project", null);

            if (request.ExpectedEndDate.Date < request.ExpectedStartDate.Date)
                return new ResponseMediator("End date must be greater or equal than the start date", null);

            //if (request.ExpectedEndDate.Date < DateTime.Now.Date)
            //    return new ResponseMediator("End date must be greater or equal than the current time", null);

            var phaseCheck = new Domain.Entities.Phase() { Title = request.Title, Description = request.Description, ExpectedStartDate = request.ExpectedStartDate, ExpectedEndDate = request.ExpectedEndDate };
            var phases = new List<Domain.Entities.Phase>();
            phases.AddRange(phase.Project.Phases);
            var phaseDelete = phases.FirstOrDefault(x => x.Id == request.Id);
            if (phaseDelete != null)
            {
                phases.Remove(phaseDelete);
            }
            var errorMessage = phaseCheck.IsValidExpectDate(phases);
            if (!string.IsNullOrEmpty(errorMessage))
                return new ResponseMediator(errorMessage, null, 400);

            foreach(var l in phase.Project.Phases.OrderByDescending(c => c.ExpectedStartDate))
            {
                if(l.ActualStartDate.HasValue && l.Id != request.Id && request.ExpectedStartDate < l.ActualStartDate )
                    return new ResponseMediator("Start date must be greater or equal running and completed phases ", null);
            }

            phase.Title = request.Title;
            phase.Description = request.Description;
            phase.ExpectedStartDate = request.ExpectedStartDate;
            phase.ExpectedEndDate = request.ExpectedEndDate;
            _unitOfWork.Phases.Update(phase);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", phase);
        }
    }
}
