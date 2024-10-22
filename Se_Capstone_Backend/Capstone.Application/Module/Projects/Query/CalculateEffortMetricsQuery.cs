using Capstone.Application.Module.Projects.Request;
using Capstone.Application.Module.Projects.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.Query
{
    public class CalculateEffortMetricsQuery : IRequest<ProjectEffortMetricsResponse>
    {
        public string ProjectName { get; set; } = string.Empty;
        public List<TaskEffort> Tasks { get; set; } = new List<TaskEffort>();
        public bool IsCalculateDetails { get; set; } = false;

    }

}
