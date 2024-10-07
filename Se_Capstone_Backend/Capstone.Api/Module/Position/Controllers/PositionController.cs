using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Application.Module.Permissions.Query;
using Capstone.Application.Module.Position.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Api.Module.Position.Controllers
{
    [Route("api/positions")]
    [ApiController]
    public class PositionController : BaseController
    {
        private readonly IMediator _mediator;

        public PositionController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetListPositionQuery());
            return ResponseOk(dataResponse: result.Data);
        }
    }
}
