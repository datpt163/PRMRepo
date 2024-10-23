using Capstone.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Domain.Entities
{
    [Table("logEntries")]
    public class LogEntry : BaseEntity

    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string? Title { get; set; } 
        [MaxLength(100)]
        public string? ErrorMessage { get; set; }
        public bool IsChecked { get; set; }
    }
}
