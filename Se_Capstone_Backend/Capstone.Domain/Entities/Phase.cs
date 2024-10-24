using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Capstone.Domain.Entities
{
    [Table("phases")]
    public class Phase
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; } = string.Empty;
        public DateTime ExpectedStartDate { get; set; }
        public DateTime ExpectedEndDate { get; set;}
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        [JsonIgnore]
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Project Project { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Issue> Issues { get; set; } = new List<Issue>();
    }
}
