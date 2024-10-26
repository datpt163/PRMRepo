using Capstone.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

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
        public string IsValidExpectDate(ICollection<Phase> phases)
        {
            foreach (var phase in phases)
            {
                if (ExpectedStartDate < phase.ExpectedEndDate && ExpectedEndDate > phase.ExpectedStartDate)
                {
                    return $"The time range from {ExpectedStartDate:dd/MM/yyyy} to {ExpectedEndDate:dd/MM/yyyy} " +
                       $"overlaps with phase '{phase.Title}'";
                }
            }
            return "";
        }
      
    }
}
