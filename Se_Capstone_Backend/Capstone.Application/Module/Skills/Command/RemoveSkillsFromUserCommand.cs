using MediatR;


namespace Capstone.Application.Module.Skills.Command
{
    public class RemoveSkillsFromUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public List<Guid> SkillIds { get; set; } = new List<Guid>();
    }

}
