using MediatR;
using Capstone.Application.Module.Skills.Response;
using Capstone.Application.Common.Paging;

namespace Capstone.Application.Module.Skills.Query
{
    public class GetSkillsListQuery : PagingQuery, IRequest<PagingResultSP<SkillDto>>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
