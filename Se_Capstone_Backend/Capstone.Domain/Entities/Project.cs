using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Domain.Entities
{
    [Table("projects")]
    public class Project
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Code { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Staff> Staffs { get; set; } = new List<Staff>();
        public ICollection<Sprint> Sprints { get; set; } = new List<Sprint>();
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }
}
