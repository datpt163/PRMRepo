using AutoMapper;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Query;
using Capstone.Application.Module.Projects.Response;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.QueryHandle
{
    public class GetDetailProjectQueryHandle : IRequestHandler<GetDetailProjectQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDetailProjectQueryHandle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseMediator> Handle(GetDetailProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.Projects.Find(x => x.Id == request.Id).Include(c => c.Lead).ThenInclude(c => c.User).Include(c => c.Issues).FirstOrDefaultAsync();

            if (project == null) 
                return new ResponseMediator("Project not found", null, 404);

            var projectMapper = _mapper.Map<ProjectDTO>(project);
            return new ResponseMediator("", projectMapper);
        }
    }
}
