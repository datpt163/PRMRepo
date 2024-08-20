using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Domain.Entities
{
    [Table("sprints")]
    public class Sprint
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
