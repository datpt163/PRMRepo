using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Status.Command;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Status.CommandHandle
{
    public class CreateStatusCommandHandle : IRequestHandler<CreateStatusCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateStatusCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
                return new ResponseMediator("Name empty", null, 400);

            if (string.IsNullOrEmpty(request.Color))
                return new ResponseMediator("Color empty", null, 400);

            var project = _unitOfWork.Projects.Find(x => x.Id == request.ProjectId).Include(c => c.Statuses).FirstOrDefault();
            if (project == null)
                return new ResponseMediator("Project not found", null, 404);

            if (project.Statuses.Select(x => x.Name.Trim().ToUpper()).Contains(request.Name.Trim().ToUpper()))
                return new ResponseMediator("This Status is availble", null, 400);

            var position = 1;
            if(project.Statuses.Count() != 0) { 
                position = project.Statuses.Count() + 1;
            }

            var status = new Capstone.Domain.Entities.Status() { Name = request.Name, ProjectId = request.ProjectId, Description = request.Description,Color = request.Color, Position = position};
            _unitOfWork.Statuses.Add(status);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", status);
        }
    }
}
