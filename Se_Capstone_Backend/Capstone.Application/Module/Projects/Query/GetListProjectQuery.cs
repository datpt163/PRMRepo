using Capstone.Application.Common.Paging;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Response;
using Capstone.Application.Module.Users.Response;
using MediatR;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.Query
{
    public class GetListProjectQuery : IRequest<PagingResultSP<ProjectDTO>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public bool? IsVisible {  get; set; }

        public GetListProjectQuery(int? pageIndex, int? pageSize, bool? isVisible)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IsVisible = isVisible;
        }
    }
}
