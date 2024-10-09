using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Labels.Query;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Labels.QueryHandle
{
    public class GetListLabelQueryHandle : IRequestHandler<GetListLabelQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetListLabelQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(GetListLabelQuery request, CancellationToken cancellationToken)
        {
            var labels = await _unitOfWork.Labels.GetQuery().ToListAsync();

            if (request.projectId.HasValue)
                labels = labels.Where(x => x.ProjectId == request.projectId).ToList();

            return new ResponseMediator("",labels);
        }
    }
}
