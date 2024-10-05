using Capstone.Application.Common.Paging;
using Capstone.Application.Module.Applicants.Response;
using MediatR;
using System;
using System.Collections.Generic;

namespace Capstone.Application.Module.Applicants.Query
{
    public class GetApplicantListQuery : PagingQuery, IRequest<PagingResultSP<ApplicantDto>>
    {
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public bool? IsOnBoard { get; set; } = null;
        public DateTime? StartDateFrom { get; set; } = null;
        public DateTime? StartDateTo { get; set; } = null;

        public List<Guid> JobIds { get; set; } = new List<Guid>();
        public List<Guid> MainJobIds { get; set; } = new List<Guid>();
    }
}
