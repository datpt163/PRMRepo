using Capstone.Domain.Entities;
using Capstone.Application.Module.Jobs.Command;
using System;
using System.Threading;
using System.Threading.Tasks;
using Capstone.Infrastructure.Repository;
using MediatR;

namespace Capstone.Application.Module.Jobs.CommandHandle
{
    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, bool>
    {
        private readonly IRepository<Job> _jobRepository;

        public DeleteJobCommandHandler(IRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public async Task<bool> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = _jobRepository.GetQuery().FirstOrDefault(x=> x.Id ==request.Id);
            if (job == null || job.IsDeleted)
            {
                return false;
            }

            job.IsDeleted = true;
            _jobRepository.Add(job);
            return true;
        }
    }
}
