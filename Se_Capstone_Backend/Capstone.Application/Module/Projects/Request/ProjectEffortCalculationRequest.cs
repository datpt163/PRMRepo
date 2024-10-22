using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.Request
{
    public class ProjectEffortCalculationRequest
    {
        public bool IsCalculateDetails { get; set; } = false;
        public string ProjectName { get; set; } = string.Empty;
        public List<TaskEffort> Tasks { get; set; } = new List<TaskEffort>();
    }
    public class TaskEffort
    {
        public Guid? UserId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public int EstimatedTime { get; set; }
        public int ActualTime { get; set; }
        public int PercentDone { get; set; }
    }
}
