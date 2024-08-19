using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Domain.Entities
{
    public class Staff
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; } 
        public String CreatedBy { get; set; } = String.Empty;
        public String UpdateBy { get; set; } = String.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; } = new User();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<LeaveLog> LeaveLogs { get; set; } = new List<LeaveLog>();
        public ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
    }
}
