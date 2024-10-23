using Capstone.Application.Module.Projects.Query;
using Capstone.Application.Module.Projects.Request;
using Capstone.Application.Module.Projects.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Application.Module.Projects.Handlers
{
    public class GetProjectDetailsHandler : IRequestHandler<GetProjectDetailsQuery, ProjectEffortCalculationResponse>
    {
        private readonly IRepository<Project> _projectRepository;

        public GetProjectDetailsHandler(IRepository<Project> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectEffortCalculationResponse> Handle(GetProjectDetailsQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetQueryNoTracking()
            .Include(x => x.Issues.Where(issue =>
                (!request.StartTime.HasValue || issue.StartDate >= request.StartTime) &&
                (!request.EndTime.HasValue || issue.DueDate <= request.EndTime)))
            //.ThenInclude(i => i.User)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId);

            if (project == null)
            {
                throw new Exception("Project not found."); 
            }

            var response = new ProjectEffortCalculationResponse
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                Tasks = new List<TaskEffort>()
            };

            foreach (var task in project.Issues)
            {
                response.Tasks.Add(new TaskEffort
                {
                    //UserId = task.UserId,
                    //UserName = task.UserName,
                    EstimatedTime = task.EstimatedTime ?? 0,
                    //ActualTime = task.ActualTime,
                    PercentDone = task.PercentDone
                });
            }

            return response;
        }


    }
}
