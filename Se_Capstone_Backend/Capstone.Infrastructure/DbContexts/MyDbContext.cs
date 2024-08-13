using Capstone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.DbContexts
{
    public class MyDbContext
    {
        public static List<User> Users { get; set; } = new List<User>
         {
         };
    }
}
