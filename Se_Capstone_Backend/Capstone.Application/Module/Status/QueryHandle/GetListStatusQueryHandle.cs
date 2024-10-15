using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Labels.Query;
using Capstone.Application.Module.Status.Query;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            if (!request.projectId.HasValue)
                return new ResponseMediator("Project id null", null);

            var statuses = await _unitOfWork.Statuses.GetQuery(x => x.ProjectId == request.projectId).Include(x => x.Issues).OrderBy(x => x.Position).Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Position = x.Position,
                Color = x.Color,
                IssueCount = x.Issues.Count,
            }).ToListAsync();
            return new ResponseMediator("", statuses);
        }
    }
}
