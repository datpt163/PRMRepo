using Capstone.Application.Module.Projects.Query;
using Capstone.Application.Module.Projects.Response;
using Capstone.Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.QueryHandle
{
    public class CalculateEffortMetricsHandler : IRequestHandler<CalculateEffortMetricsQuery, ProjectEffortMetricsResponse>
    {
        public Task<ProjectEffortMetricsResponse> Handle(CalculateEffortMetricsQuery request, CancellationToken cancellationToken)
        {
            var metrics = new ProjectEffortMetricsResponse { ProjectName = request.ProjectName };

            metrics.TotalEstimatedTime = request.Tasks.Sum(task => task.EstimatedTime);
            metrics.TotalActualTime = request.Tasks.Sum(task => task.ActualTime);

            metrics.CompletionRate = EffortCalculator.CalculateCompletionRate(metrics.TotalEstimatedTime, metrics.TotalActualTime);

            if (request.IsCalculateDetails)
            {
                metrics.UserPerformance = request.Tasks
                    .GroupBy(task => task.UserId) 
                    .Select(group => new UserPerformance
                    {
                        UserId = group.Key,
                        UserName = group.First().UserName, 
                        CompletionRate = EffortCalculator.CalculateCompletionRate(group.Sum(task => task.EstimatedTime), group.Sum(task => task.ActualTime))
                    }).ToList();
            }

            return Task.FromResult(metrics);
        }
    }

}
