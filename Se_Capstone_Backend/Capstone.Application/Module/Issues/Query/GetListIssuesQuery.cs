using Capstone.Application.Common.Paging;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Issues.Query
{
    public class GetListIssuesQuery : IRequest<PagingResultSP<Application.Module.Issues.DTO.IssueDTO>>
    {
        public Guid? ProjectId { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? Index { get; set; }
        public string? Title { get; set; }
        public Priority? Priority { get; set; }
        public Guid? AssigneeId { get; set; }
        public Guid? ReporterId { get; set; }
        public Guid? StatusId { get; set; }
        public Guid? LabelId { get; set; }
        public Guid? PhaseId { get; set; }
    }
}
