using Capstone.Application.Module.Skills.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Skills.CommandHandle
{
    public class AddSkillToUserHandler : IRequestHandler<AddSkillToUserCommand, bool>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Skill> _skillRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddSkillToUserHandler(IRepository<User> userRepository, IRepository<Skill> skillRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddSkillToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetQuery()
                .Include(u => u.Skills)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return false;
            }

            var skill = await _skillRepository.GetQueryNoTracking().FirstOrDefaultAsync(x=> x.Id == request.SkillId, cancellationToken);
            if (skill == null)
            {
                return false;
            }
            user.Skills ??= new List<Skill>();

            if (user.Skills.Any(s => s.Id == skill.Id))
            {
                return false;
            }

            user.Skills.Add(skill);

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }


}
