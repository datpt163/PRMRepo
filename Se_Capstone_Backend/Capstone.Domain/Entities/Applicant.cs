using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Domain.Entities
{
    [Table("applicants")]
    public class Applicant
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        public DateOnly StartDate { get; set; }
        [MaxLength(100)]
        public string PhoneNumber { get; set; } = string.Empty;
        [MaxLength(100)]
        public string CvLink { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdateAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsOnBoard { get; set; }
        [MaxLength(100)]
        public string CreatedBy { get; set; } = string.Empty;
        [MaxLength(100)]
        public string UpdatedBy { get; set; } = string.Empty;
        public Guid StaffId { get; set; }
        public Staff? Staff { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
