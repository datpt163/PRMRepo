using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Domain.Entities
{
    public class Sprint
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Issue> Issues { get;} = new List<Issue>();
        public ICollection<Project> Projects { get; } = new List<Project>();
    }
}
