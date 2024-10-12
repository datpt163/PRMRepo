using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Labels.Query;
using Capstone.Application.Module.Status.Query;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Status.QueryHandle
{
    public class GetListStatusQueryHandle : IRequestHandler<GetListStatusQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetListStatusQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(GetListStatusQuery request, CancellationToken cancellationToken)
        {
            var statuses = await _unitOfWork.Statuses.GetQuery().ToListAsync();

            if (request.projectId.HasValue)
                statuses = statuses.Where(x => x.ProjectId == request.projectId).OrderBy(x => x.Position).ToList();

            return new ResponseMediator("", statuses);
        }
    }
}
