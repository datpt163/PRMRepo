using AutoMapper;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Issues.DTO;
using Capstone.Application.Module.Issues.Query;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Issues.QueryHandle
{
    public class GetListIssueQueryHandle : IRequestHandler<GetListIssuesQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetListIssueQueryHandle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(GetListIssuesQuery request, CancellationToken cancellationToken)
        {
            if(!request.ProjectId.HasValue)
                return new ResponseMediator("User  not found", null, 404);

            var project = await _unitOfWork.Projects.Find(x => x.Id == request.ProjectId).Include(c => c.Statuses).ThenInclude(c => c.Issues).ThenInclude(c => c.Assignee).FirstOrDefaultAsync();
            if(project == null)
                return new ResponseMediator("Project  not found", null, 404);

            var issueIds = project.Statuses.SelectMany(x => x.Issues).ToList().Select(c => c.Id);
            var issues = _unitOfWork.Issues.Find(x => issueIds.Contains(x.Id)).Include(c => c.Phase).Include(c => c.Label).Include(c => c.Status).Include(c => c.LastUpdateBy).Include(c => c.ParentIssue).Include(c => c.Reporter).Include(c => c.Assignee).Include(c => c.SubIssues).Include(c => c.Comments).OrderByDescending(x => x.Index);
            return new ResponseMediator("", _mapper.Map<List<IssueDTO>>(issues));
        }
    }
}
