using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Capstone.Domain.Enums;
namespace Capstone.Domain.Entities
{
    [Table("issues")]
    public class Issue
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public int Index {  get; set; }
        public string Subject { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Percentage { get; set; } = 0;
        public Priority Priority { get; set; }
        public int? EstimatedTime { get; set; }
        public int PercentDone { get; set; }
        public int Position { get; set; }
        public Guid? ParentIssueId { get; set; }
        public Guid AssignedId { get; set; }
        public Guid? LastUpdateById { get; set; }
        public Guid ProjectId { get; set; }
        public Guid StatusId { get; set; }
        public Guid LabelId { get; set; }
        public Label? Label { get; set; } 
        public Status? Status { get; set; } 
        public Project? Project { get; set; }
        public User? LastUpdateBy { get; set; }
        public Issue? ParentIssue { get; set; }
        public List<Issue> SubIssues { get; set; } = new List<Issue>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
