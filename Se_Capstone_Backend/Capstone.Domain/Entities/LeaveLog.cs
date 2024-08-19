using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Domain.Entities
{
    public class LeaveLog
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public bool IsFullDay { get; set; }
        public bool IsPaid { get; set; }
        public bool IsApprove { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsChecked { get; set; }
        public bool IsDeleted { get; set; }

        public Guid StaffId { get; set; }
        public Staff Staff { get; set; } = new Staff();
    }
}
