using Capstone.Domain.Entities.Common;
using Capstone.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Domain.Entities
{
    [Table("projects")]
    public class Project : BaseEntity

    {
        public Project()
        {
        }

        public Project(string name, string code, string description, DateTime startDate, DateTime endDate, Guid? leadId, bool isVisible)
        {
            Name = name;
            Code = code;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Status = ProjectStatus.NotStarted;
            CreatedAt = DateTime.Now;
            LeadId = leadId;
            IsVisible = isVisible;
        }

        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Code { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
        public bool IsVisible { get; set; } = false;
        public Guid? LeadId { get; set; }
        public User? Lead { get; set; }
        public ICollection<Status> Statuses { get; set; } = new List<Status>();
        public ICollection<Label> Labels { get; set; } = new List<Label>(); 
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();

    }
}
