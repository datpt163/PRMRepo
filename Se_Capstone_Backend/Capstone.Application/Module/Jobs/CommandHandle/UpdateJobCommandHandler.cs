using Capstone.Domain.Entities;
using Capstone.Application.Module.Jobs.Command;
using Capstone.Application.Module.Jobs.Response;
using System;
using System.Threading;
using System.Threading.Tasks;
using Capstone.Infrastructure.Repository;
using MediatR;

namespace Capstone.Application.Module.Jobs.CommandHandle
{
    public class UpdateJobCommandHandler : IRequestHandler<UpdateJobCommand, JobDto?>
    {
        private readonly IRepository<Job> _jobRepository;

        public UpdateJobCommandHandler(IRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<JobDto?> Handle(UpdateJobCommand request, CancellationToken cancellationToken)
        {
            var job = _jobRepository.GetQuery().FirstOrDefault(x=> x.Id ==request.Id);
            if (job == null || job.IsDeleted)
            {
                return null;
            }

            job.Title = request.Title;
            job.Description = request.Description;
            job.UpdateAt = DateTime.UtcNow;
            job.UpdatedBy = "Admin";

            _jobRepository.Update(job);

            return new JobDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                CreatedAt = job.CreatedAt,
                UpdateAt = job.UpdateAt,
                IsDeleted = job.IsDeleted,
                CreatedBy = job.CreatedBy,
                UpdatedBy = job.UpdatedBy
            };
        }
    }
}
