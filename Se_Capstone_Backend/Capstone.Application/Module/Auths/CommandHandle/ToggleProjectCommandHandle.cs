using AutoMapper;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Auths.Command;
using Capstone.Application.Module.Projects.Response;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.CommandHandle
{
    public class ToggleProjectCommandHandle : IRequestHandler<ToggleProjectCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ToggleProjectCommandHandle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(ToggleProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _unitOfWork.Projects.Find(x => x.Id == request.Id).Include(c => c.Lead).FirstOrDefault();
            if (project == null)
                return new ResponseMediator("Project not found", null, 404);

            project.IsVisible = !project.IsVisible;
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();
            var projectMapper = _mapper.Map<ProjectDTO>(project);
            return new ResponseMediator("", projectMapper);

        }
    }
}
