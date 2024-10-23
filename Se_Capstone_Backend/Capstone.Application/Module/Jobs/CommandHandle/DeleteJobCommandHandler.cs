using Capstone.Domain.Entities;
using Capstone.Application.Module.Jobs.Command;
using Capstone.Infrastructure.Repository;
using MediatR;

namespace Capstone.Application.Module.Jobs.CommandHandle
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, bool>
    {
        private readonly IRepository<Job> _jobRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteJobCommandHandler(IRepository<Job> jobRepository, IUnitOfWork unitOfWork)
        {
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = _jobRepository.GetQuery().FirstOrDefault(x=> x.Id ==request.Id);
            if (job == null || job.IsDeleted)
            {
                return false;
            }
            job.IsDeleted = true;
            await _jobRepository.UpdateAsync(job);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
