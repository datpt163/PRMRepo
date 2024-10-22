using Capstone.Application.Module.Projects.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.Response
{
    public class ProjectEffortCalculationResponse
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public List<TaskEffort> Tasks { get; set; } = new List<TaskEffort>();
    }
}
