using Capstone.Application.Module.Skills.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Skills.CommandHandle
{
    public class RemoveSkillFromUserHandler : IRequestHandler<RemoveSkillFromUserCommand, bool>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveSkillFromUserHandler(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveSkillFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetQuery()
                .Include(x => x.Skills)
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return false;
            }

            if (user.Skills == null || !user.Skills.Any())
            {
                return false;
            }

            var skill = user.Skills.FirstOrDefault(s => s.Id == request.SkillId);

            if (skill == null)
            {
                return false;
            }

            user.Skills.Remove(skill);

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

    }

}
