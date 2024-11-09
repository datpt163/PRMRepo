using AutoMapper;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Issues.DTO;
using Capstone.Application.Module.Status.Query;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Application.Module.Status.QueryHandle
{
    public class GetListStatusQueryHandle : IRequestHandler<GetListStatusQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetListStatusQueryHandle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(GetListStatusQuery request, CancellationToken cancellationToken)
        {
            if (!request.projectId.HasValue)
                return new ResponseMediator("Project id null", null);

            var statuses = await _unitOfWork.Statuses.GetQuery(x => x.ProjectId == request.projectId).
                Include(x => x.Issues)
                .OrderBy(x => x.Position).Select(x => new
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

    public class GetListStatusKanbanHandle : IRequestHandler<GetListStatusKanbanQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetListStatusKanbanHandle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(GetListStatusKanbanQuery request, CancellationToken cancellationToken)
        {
            if (!request.projectId.HasValue)
                return new ResponseMediator("Project id null", null);

            var statuses = await _unitOfWork.Statuses.GetQuery(x => x.ProjectId == request.projectId).
                Include(x => x.Issues.Where(c => c.ParentIssueId == null)).ThenInclude(c => c.Phase).
                 Include(x => x.Issues).ThenInclude(c => c.Label).
                 Include(x => x.Issues).ThenInclude(c => c.LastUpdateBy).
                 Include(x => x.Issues).ThenInclude(c => c.Reporter).
                 Include(x => x.Issues).ThenInclude(c => c.Assignee)
                .OrderBy(x => x.Position).Select(x => new
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Position = x.Position,
                    Color = x.Color,
                    Issues = _mapper.Map<List<IssueDTO>>(x.Issues.OrderBy(x => x.Position)),
                    IssueCount = x.Issues.Count,
                }).ToListAsync();
            return new ResponseMediator("", statuses);
        }
    }
}
