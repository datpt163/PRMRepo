using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Phase.Query;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Phase.QueryHandle
{
    public class GetListPhaseQueryHandle : IRequestHandler<GetListPhaseQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetListPhaseQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(GetListPhaseQuery request, CancellationToken cancellationToken)
        {
            var phases = await _unitOfWork.Phases.GetQuery(x => x.ProjectId == request.ProjectId).OrderBy(c => c.ExpectedStartDate).ToListAsync();
            return new ResponseMediator("", phases);
        }
    }
}
