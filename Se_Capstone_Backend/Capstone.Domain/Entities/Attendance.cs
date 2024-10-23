using Capstone.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Domain.Entities
{
    [Table("attendances")]
    public class Attendance : BaseEntity

    {
        public Guid Id { get; set; }
        public bool IsCheckIn { get; set; }
        public DateTime? TimeStamp { get; set; }
        public DateTime CreateAt { get; set; }
        public bool IsChecked { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
