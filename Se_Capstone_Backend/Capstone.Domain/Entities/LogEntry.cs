using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Domain.Entities
{
    [Table("logEntries")]
    public class LogEntry
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(100)]
        public string ErrorMessage { get; set; } = string.Empty;
        public DateOnly CreateAt { get; set; }
        public bool IsChecked { get; set; }
    }
}
