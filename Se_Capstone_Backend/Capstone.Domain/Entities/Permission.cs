using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Capstone.Domain.Entities
{
    [Table("permissions")]
    public class Permission
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public Guid GroupPermissionId { get; set; }
        [JsonIgnore]
        public GroupPermission? GroupPermission { get; set; }
        [JsonIgnore]
        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
