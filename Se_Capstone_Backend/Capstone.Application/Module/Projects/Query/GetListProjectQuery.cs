using Capstone.Application.Common.Paging;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Response;
using Capstone.Application.Module.Users.Response;
using Capstone.Domain.Enums;
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
        public ProjectStatus? Status { get; set; }
        public string Token { get; set; } = string.Empty;

        public GetListProjectQuery(int? pageIndex, int? pageSize, bool? isVisible, ProjectStatus? status, string token)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            IsVisible = isVisible;
            Status = status;
            Token = token;
        }
    }
}
