using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
