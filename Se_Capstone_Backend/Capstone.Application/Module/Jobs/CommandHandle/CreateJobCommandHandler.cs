using Capstone.Domain.Entities;
using Capstone.Application.Module.Jobs.Command;
using Capstone.Application.Module.Jobs.Response;
using Capstone.Infrastructure.Repository;
using MediatR;

namespace Capstone.Application.Module.Jobs.CommandHandle
{
    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, JobDto>
    {
        private readonly IRepository<Job> _jobRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateJobCommandHandler(IRepository<Job> jobRepository, IUnitOfWork unitOfWork)
        {
            _jobRepository = jobRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<JobDto> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var job = new Job
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                CreatedAt = DateTime.Now,
                CreatedBy = null,
                IsDeleted = false
            };

             _jobRepository.Add(job);
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
