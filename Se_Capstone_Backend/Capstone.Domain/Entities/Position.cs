using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Capstone.Domain.Entities
{
    [Table("positions")]
    public class Position
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(250)]
        public string? Description {  get; set; }
        [JsonIgnore]
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
