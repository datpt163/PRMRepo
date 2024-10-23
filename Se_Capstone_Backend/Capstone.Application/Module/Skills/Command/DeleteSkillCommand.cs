using MediatR;

namespace Capstone.Application.Module.Skills.Command
{
    public class DeleteSkillCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
