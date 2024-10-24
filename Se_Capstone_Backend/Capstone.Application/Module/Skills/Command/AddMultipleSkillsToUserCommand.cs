using Capstone.Application.Module.Skills.Response;
using MediatR;


namespace Capstone.Application.Module.Skills.Command
{
    public class AddMultipleSkillsToUserCommand : IRequest<AddMultipleSkillsResponse>
    {
        public Guid UserId { get; set; }
        public List<Guid> SkillIds { get; set; } = new List<Guid>();

    }
}
