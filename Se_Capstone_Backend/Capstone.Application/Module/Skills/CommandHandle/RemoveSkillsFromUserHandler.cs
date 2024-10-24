using Capstone.Application.Module.Skills.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Skills.CommandHandle
{
    public class RemoveSkillsFromUserHandler : IRequestHandler<RemoveSkillsFromUserCommand, bool>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveSkillsFromUserHandler(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveSkillsFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetQuery()
                .Include(x => x.Skills)
                .FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user == null)
            {
                return false;
            }

            var skillsToRemove = user.Skills
                .Where(s => request.SkillIds.Contains(s.Id))
                .ToList();

            if (!skillsToRemove.Any())
            {
                return false;
            }

            foreach (var skill in skillsToRemove)
            {
                user.Skills.Remove(skill);
            }

            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }


    }

}
