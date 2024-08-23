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
    [Table("groupPermissions")]
    public class GroupPermission
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Permission> Permissions { get; set;} = new List<Permission>();
    }
}
