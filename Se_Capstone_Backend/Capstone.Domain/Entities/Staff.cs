using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Domain.Entities
{
    [Table("staffs")]
    public class Staff
    {
        public Guid Id { get; set; }
        public DateTime? StartDate { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? UpdateBy { get; set; } 
        public User? User { get; set; }
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<LeaveLog> LeaveLogs { get; set; } = new List<LeaveLog>();
        public ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }
}
