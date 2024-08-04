using Capstone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.DbContext
{
    public class MyDbContext
    {
        public static List<User> Users { get; set; } = new List<User>
         {
             new User(){  Id = Guid.Parse("add4df29-f642-468e-bdf3-9acac63c2fb6") , Email = "dat", Password = "123" }
         };
    }
}
