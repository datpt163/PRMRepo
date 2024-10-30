using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Capstone.Domain.Entities
{
    [Table("groupPermissions")]
    public class GroupPermission
    {
        public Guid Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
   
        public ICollection<Permission> Permissions { get; set;} = new List<Permission>();
    }
}
