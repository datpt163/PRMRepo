using Capstone.Application.Module.Skills.Response;
using MediatR;

namespace Capstone.Application.Module.Skills.Query
{
    public class GetSkillsByUserIdQuery : IRequest<List<SkillDto>>
    {
        public Guid UserId { get; set; }
    }


}
