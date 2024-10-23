using Capstone.Domain.Entities;
using Capstone.Application.Module.Skills.Command;
using System;
using System.Threading;
using System.Threading.Tasks;
using Capstone.Infrastructure.Repository;
using MediatR;

namespace Capstone.Application.Module.Skills.CommandHandle
{
    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, bool>
    {
        private readonly IRepository<Skill> _skillRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSkillCommandHandler(IRepository<Skill> skillRepository, IUnitOfWork unitOfWork)
        {
            _skillRepository = skillRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = _skillRepository.GetQuery().FirstOrDefault(x=> x.Id ==request.Id);
            if (skill == null || skill.IsDeleted)
            {
                return false;
            }
            skill.IsDeleted = true;
            await _skillRepository.UpdateAsync(skill);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
