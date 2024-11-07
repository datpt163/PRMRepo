﻿using Capstone.Application.Common.ResponseMediator;
using MediatR;

namespace Capstone.Application.Module.Auths.Query
{
    public class GetListRoleQuery : IRequest<ResponseMediator>
    {
        public string? Name { get; set; }
    }
}
