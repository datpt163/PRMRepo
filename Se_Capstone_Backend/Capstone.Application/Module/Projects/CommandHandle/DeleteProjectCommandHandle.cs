using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Command;
using Capstone.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.CommandHandle
{
    public class DeleteProjectCommandHandle : IRequestHandler<DeleteProjectCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProjectCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseMediator> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _unitOfWork.Projects.FindOne(x => x.Id == request.Id);

            if (project == null) 
                return new ResponseMediator("Project not found", null, 404);

            _unitOfWork.Projects.Remove(project);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", null);
        }
    }
}
