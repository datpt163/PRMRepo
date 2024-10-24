using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Api.Common.ResponseApi.Model;
using Capstone.Application.Module.Labels.Command;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Capstone.Api.Module.Phases.Controllers
{
    [Route("api/phases")]
    [ApiController]
    public class PhaseController : BaseController
    {
        private readonly IMediator _mediator;

        public PhaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost]
        //[SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        //[Authorize(Roles = "ADD_PHASE")]
        //public async Task<IActionResult> CreateLabel([FromBody] CreateLabelCommand request)
        //{
        //    var result = await _mediator.Send();
        //    if (string.IsNullOrEmpty(result.ErrorMessage))
        //        return ResponseOk(result.Data);
        //    else
        //    {
        //        if (result.StatusCode == 404)
        //            return ResponseNotFound(messageResponse: result.ErrorMessage);
        //        return ResponseBadRequest(messageResponse: result.ErrorMessage);

        //    }
        //}

    }
}
