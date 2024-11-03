using AutoMapper;
using Capstone.Application.Common.Paging;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Issues.DTO;
using Capstone.Application.Module.Issues.Query;
using Capstone.Application.Module.Projects.Response;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Issues.QueryHandle
{
    public class GetListIssueQueryHandle : IRequestHandler<GetListIssuesQuery, PagingResultSP<Application.Module.Issues.DTO.IssueDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetListIssueQueryHandle(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagingResultSP<DTO.IssueDTO>> Handle(GetListIssuesQuery request, CancellationToken cancellationToken)
        {
            if(!request.ProjectId.HasValue)
                return new PagingResultSP<Application.Module.Issues.DTO.IssueDTO> () { ErrorMessage = "Project not found" };

            var project = await _unitOfWork.Projects.Find(x => x.Id == request.ProjectId).Include(c => c.Statuses).ThenInclude(c => c.Issues).ThenInclude(c => c.Assignee).FirstOrDefaultAsync();
            if(project == null)
                return new PagingResultSP<Application.Module.Issues.DTO.IssueDTO>() { ErrorMessage = "Project not found" };

            var issueIds = project.Statuses.SelectMany(x => x.Issues).ToList().Select(c => c.Id);
            var issues = _unitOfWork.Issues.Find(x => issueIds.Contains(x.Id)).Include(c => c.Phase).Include(c => c.Label).Include(c => c.Status).Include(c => c.LastUpdateBy).Include(c => c.ParentIssue).Include(c => c.Reporter).Include(c => c.Assignee).Include(c => c.SubIssues).Include(c => c.Comments).OrderByDescending(x => x.Index).ToList();
            if (request.Index.HasValue)
                issues = issues.Where(x => x.Index == request.Index.Value).ToList();

            if (request.Index.HasValue)
                issues = issues.Where(x => x.Index == request.Index.Value).ToList();
            if (request.Title != null)
                issues = issues.Where(x => x.Title.Trim().ToUpper().Contains(request.Title.Trim().ToUpper())).ToList();
            if(request.Priority != null)
            {
                if (request.Priority.HasValue && ((int)request.Priority < 1 || (int)request.Priority > 5))
                    return new PagingResultSP<Application.Module.Issues.DTO.IssueDTO>() { ErrorMessage = "Priority must be between 1 and 5" };
                else
                    issues = issues.Where(x => x.Priority == request.Priority).ToList();
            }

            if (request.AssigneeId.HasValue)
            {
                //if (_unitOfWork.Users.FindOne(x => x.Id == request.AssigneeId.Value) == null)
                //    return new ResponseMediator("Assignee not found", null, 404);
                //else
                    issues = issues.Where(x => x.AssigneeId == request.AssigneeId).ToList();
            }

            if (request.ReporterId.HasValue)
            {
                //if (_unitOfWork.Users.FindOne(x => x.Id == request.ReporterId.Value) == null)
                //    return new ResponseMediator("Assignee not found", null, 404);
                //else
                    issues = issues.Where(x => x.ReporterId == request.ReporterId).ToList();
            }

            if (request.StatusId.HasValue)
            {
                //if (_unitOfWork.Statuses.FindOne(x => x.Id == request.StatusId.Value) == null)
                //    return new ResponseMediator("Assignee not found", null, 404);
                //else
                    issues = issues.Where(x => x.StatusId == request.StatusId).ToList();
            }

            if (request.LabelId.HasValue)
            {
                issues = issues.Where(x => x.LabelId == request.LabelId).ToList();
            }

            if (request.PhaseId.HasValue)
            {
                issues = issues.Where(x => x.PhaseId == request.PhaseId).ToList();
            }

            if (request.PageIndex.HasValue && request.PageSize.HasValue)
            {
                if (request.PageIndex.Value < 1 || request.PageSize.Value < 0)
                    return new PagingResultSP<Application.Module.Issues.DTO.IssueDTO>() { ErrorMessage = "PageIndex, PageSize must >= 0" };

                int skip = (request.PageIndex.Value - 1) * request.PageSize.Value;
                var IssuePaging = issues.OrderByDescending(c => c.Index).Skip(skip).Take(request.PageSize.Value).ToList();
                var totalCount = issues.Count();
                var result = new PagingResultSP<Application.Module.Issues.DTO.IssueDTO>((_mapper.Map<List<Application.Module.Issues.DTO.IssueDTO>>(IssuePaging)).OrderByDescending(c => c.Index).ToList(), totalCount, request.PageIndex.Value, request.PageSize.Value);
                return result;
            }

            return new PagingResultSP<Application.Module.Issues.DTO.IssueDTO>() { Data = (_mapper.Map<List<Application.Module.Issues.DTO.IssueDTO>>(issues)).OrderByDescending(c => c.Index).ToList()};
        }

      
     
    }
}
