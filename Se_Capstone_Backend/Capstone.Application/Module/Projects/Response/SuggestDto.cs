using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.Response
{
    public class SuggestDto
    {
        public List<Guid> UserId { get; set; } = new List<Guid>();
    }
    public class SuggestMapping
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
