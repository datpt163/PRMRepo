using MediatR;
using Capstone.Application.Module.Skills.Response;
using Capstone.Application.Common.Paging;
using Capstone.Application.Module.Applicants.Response;

namespace Capstone.Application.Module.Skills.Query
{
    public class GetSkillsListQuery : PagingQuery, IRequest<PagingResultSP<SkillDto>>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
