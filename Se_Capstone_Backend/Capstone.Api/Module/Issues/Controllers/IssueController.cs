using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Api.Common.ResponseApi.Model;
using Capstone.Api.Module.Issues.Request;
using Capstone.Api.Module.Statuses.Requests;
using Capstone.Application.Module.Issues.Command;
using Capstone.Application.Module.Issues.Query;
using Capstone.Application.Module.Status.Command;
using Capstone.Application.Module.Status.Query;
using Capstone.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Capstone.Api.Module.Issues.Controllers
{
    [Route("api/issues")]
    [ApiController]
    public class IssueController : BaseController
    {
        private readonly IMediator _mediator;

        public IssueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [Authorize(Roles = "ADD_ISSUE")]
        public async Task<IActionResult> CreateStatus([FromBody] CreateIssueRequest request)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _mediator.Send(new AddIssueCommand(token, request.Title, request.Description, request.StartDate, request.DueDate, request.Priority, request.EstimatedTime, request.ParentIssueId, request.AssigneeId, request.StatusId, request.LabelId));
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
        public async Task<IActionResult> GetListStatus(Guid? projectId, int? pageIndex, int? pageSize, int? index, string? title, Priority? priority, Guid? assigneeId, Guid? reporterId, Guid? statusId, Guid? labelId, Guid? phaseId)
        {
            var result = await _mediator.Send(new GetListIssuesQuery() { ProjectId = projectId, PageIndex = pageIndex, PageSize = pageSize, Index = index, Title = title, Priority = priority, AssigneeId = assigneeId, ReporterId = reporterId, StatusId = statusId, LabelId = labelId, PhaseId = phaseId });
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(result.Data);
            else
            {
                return ResponseBadRequest(messageResponse: result.ErrorMessage);
            }
        }


        [HttpDelete("{id}")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [Authorize(Roles = "DELETE_ISSUE")]
        public async Task<IActionResult> DeleteStatus(Guid id)
        {
            var result = await _mediator.Send(new DeleteIssueCommand() { Id = id});
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            else
            {
                if (result.StatusCode == 404)
                    return ResponseNotFound(messageResponse: result.ErrorMessage);
                return ResponseBadRequest(messageResponse: result.ErrorMessage);
            }
        }

        [HttpGet("{id}")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [Authorize(Roles = "DELETE_ISSUE")]
        public async Task<IActionResult> GetDetailIssue(Guid id)
        {
            var result = await _mediator.Send(new GetDetailIssueQuery() { Id = id });
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(result.Data);
            else
            {
                if (result.StatusCode == 404)
                    return ResponseNotFound(messageResponse: result.ErrorMessage);
                return ResponseBadRequest(messageResponse: result.ErrorMessage);
            }
        }

        [HttpPut("{id}")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [Authorize(Roles = "UPDATE_ISSUE")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusRequest request)
        {
            var result = await _mediator.Send(new UpdateStatusCommand() { Id = id, Name = request.Name, Description = request.Description, Color = request.Color });
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(result.Data);
            else
            {
                if (result.StatusCode == 404)
                    return ResponseNotFound(messageResponse: result.ErrorMessage);
                return ResponseBadRequest(messageResponse: result.ErrorMessage);
            }
        }
    }
}
