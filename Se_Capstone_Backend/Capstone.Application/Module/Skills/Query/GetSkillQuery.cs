using MediatR;
using Capstone.Application.Module.Skills.Response;

namespace Capstone.Application.Module.Skills.Query
{
    public class GetSkillQuery : IRequest<SkillDto?>
    {
        public Guid Id { get; set; }
    }
}
