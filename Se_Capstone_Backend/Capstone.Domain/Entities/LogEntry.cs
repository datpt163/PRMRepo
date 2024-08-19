using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Domain.Entities
{
    public class LogEntry
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public bool IsChecked { get; set; } 
    }
}
