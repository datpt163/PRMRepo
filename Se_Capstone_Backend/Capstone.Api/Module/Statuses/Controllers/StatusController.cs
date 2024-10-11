using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Api.Common.ResponseApi.Model;
using Capstone.Api.Module.Labels.Requests;
using Capstone.Api.Module.Statuses.Requests;
using Capstone.Application.Module.Labels.Command;
using Capstone.Application.Module.Labels.Query;
using Capstone.Application.Module.Status.Command;
using Capstone.Application.Module.Status.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Capstone.Api.Module.Statuses.Controllers
{
    [Route("api/status")]
    [ApiController]
    public class StatusController : BaseController
    {
        private readonly IMediator _mediator;

        public StatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        //[Authorize(Roles = "ADD_LABEL")]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusCommand request)
        {
            var result = await _mediator.Send(request);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(result.Data);
            else
            {
                if (result.StatusCode == 404)
                    return ResponseNotFound(messageResponse: result.ErrorMessage);
                return ResponseBadRequest(messageResponse: result.ErrorMessage);

            }
        }

        [HttpGet]
        //[Authorize(Roles = "READ_LABELS")]
        public async Task<IActionResult> GetListStatus(Guid? projectId)
        {
            var result = await _mediator.Send(new GetListStatusQuery() { projectId = projectId });
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(result.Data);
            else
            {
                return ResponseBadRequest(messageResponse: result.ErrorMessage);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        //[Authorize(Roles = "DELETE_LABEL")]
        public async Task<IActionResult> DeleteLabel(Guid id, [FromBody] DeleteStatusRequest newStatus)
        {
            var result = await _mediator.Send(new DeleteStatusCommand() { Id = id, NewStatusId = newStatus.newStatusId });
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            else
            {
                if (result.StatusCode == 404)
                    return ResponseNotFound(messageResponse: result.ErrorMessage);
                return ResponseBadRequest(messageResponse: result.ErrorMessage);
            }
        }

        //[HttpPut("{id}")]
        //[SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        //[Authorize(Roles = "UPDATE_LABEL")]
        //public async Task<IActionResult> UpdateLabel(Guid id, [FromBody] UpdateLabelRequest request)
        //{
        //    var result = await _mediator.Send(new UpdateLabelCommand() { Id = id, Title = request.Title, Description = request.Description });
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
