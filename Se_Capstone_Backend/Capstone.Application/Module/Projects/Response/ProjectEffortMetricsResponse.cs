using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.Response
{
    public class ProjectEffortMetricsResponse
    {
        public string ProjectName { get; set; } = string.Empty;
        public int TotalEstimatedTime { get; set; }
        public int TotalActualTime { get; set; }
        public decimal CompletionRate { get; set; }
        public List<UserPerformance> UserPerformance { get; set; } = new List<UserPerformance>();
    }
    public class UserPerformance
    {
        public Guid? UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public decimal CompletionRate { get; set; }
    }

}
