using Capstone.Application.Module.Skills.Command;
using Capstone.Application.Module.Skills.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Skills.CommandHandle
{
    public class AddMultipleSkillsToUserHandler : IRequestHandler<AddMultipleSkillsToUserCommand, AddMultipleSkillsResponse>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Skill> _skillRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddMultipleSkillsToUserHandler(IRepository<User> userRepository, IRepository<Skill> skillRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddMultipleSkillsResponse> Handle(AddMultipleSkillsToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetQuery()
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken) ?? throw new Exception("User not found.");
            var existingSkills = new List<string>();
            var notFoundSkills = new List<Guid>();

            foreach (var skillId in request.SkillIds)
            {
                var skill = await _skillRepository.GetQueryNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == skillId, cancellationToken);

                if (skill == null)
                {
                    notFoundSkills.Add(skillId);
                    continue;
                }

                user.Skills ??= new List<Skill>();

                if (user.Skills.Any(s => s.Id == skill.Id))
                {
                    existingSkills.Add(skill.Title);
                    continue;
                }

                user.Skills.Add(skill);
            }

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var message = string.Empty;

            if (existingSkills.Any())
            {
                message += $"Skills already exist for user: {string.Join(", ", existingSkills)}. ";
            }

            if (notFoundSkills.Any())
            {
                message += $"Skills not found: {string.Join(", ", notFoundSkills)}.";
            }

            return new AddMultipleSkillsResponse(true, message);
        }
    }

}
