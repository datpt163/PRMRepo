using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Domain.Entities
{
    public class Issue
    {
        public Guid Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Percentage { get; set; } = 0; 
        public int Priority { get; set; }
        public int EstimatedTime { get; set; }
        public int PercentDone { get; set; }
        public Guid AssignedId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid StatusId { get; set; }
        public Guid LabelId { get; set; }
        public Label Label { get; set; } = new Label();
        public Status Status { get; set; } = new Status();
        public Project Project { get; set; } = new Project();
        public ICollection<Sprint> Sprints { get; } = new List<Sprint>();

    }
}
