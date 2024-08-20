using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Domain.Entities
{
    [Table("jobs")]
    public class Job
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdateAt { get; set; }
        public bool IsDeleted { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; } = string.Empty;
        [MaxLength(100)]
        public string UpdatedBy { get; set; } = string.Empty;
        public ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
    }
}
