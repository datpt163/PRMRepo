using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Api.Module.Positions.Request
{
    public class GetPositionsListRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
