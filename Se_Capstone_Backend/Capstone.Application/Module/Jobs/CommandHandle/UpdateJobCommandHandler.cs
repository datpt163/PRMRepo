using Capstone.Domain.Entities;
using Capstone.Application.Module.Jobs.Command;
using Capstone.Application.Module.Jobs.Response;
using Capstone.Infrastructure.Repository;
using MediatR;

namespace Capstone.Application.Module.Jobs.CommandHandle
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, JobDto?>
    {
        private readonly IRepository<Job> _jobRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateJobCommandHandler(IRepository<Job> jobRepository, IUnitOfWork unitOfWork)
        {
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<JobDto?> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = _jobRepository.GetQuery().FirstOrDefault(x=> x.Id ==request.Id);
            if (job == null)
            {
                return null;
            }
            if (request.Isdeleted != null)
            job.IsDeleted = (bool)request.Isdeleted;
            if(!string.IsNullOrEmpty(request.Title))
            job.Title = request.Title;
            if(!string.IsNullOrEmpty(request.Description))
            job.Description = request.Description;

            job.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            job.UpdatedBy = null;

            await _jobRepository.UpdateAsync(job);
            await _unitOfWork.SaveChangesAsync();
            return new JobDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                CreatedAt = job.CreatedAt,
                UpdatedAt = job.UpdatedAt,
                IsDeleted = job.IsDeleted,
                CreatedBy = job.CreatedBy,
                UpdatedBy = job.UpdatedBy
            };
        }
    }
}
