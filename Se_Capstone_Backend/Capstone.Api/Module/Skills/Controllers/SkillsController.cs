﻿using Capstone.Application.Module.Skills.Command;
using Capstone.Application.Module.Skills.Response;
using Capstone.Api.Common.ResponseApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using Capstone.Application.Module.Skills.Query;
using Capstone.Api.Module.Skills.Request;
using static Google.Apis.Requests.BatchRequest;

namespace Capstone.Api.Module.skills.Controllers
{
    [ApiController]
    [Route("api/skills")]
    public class skillsController : BaseController
    {
        private readonly IMediator _mediator;

        public skillsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSkillCommand command)
        {
            var skillDto = await _mediator.Send(command);
            return ResponseCreated(skillDto, "Skill created successfully");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSkillCommand command)
        {
            var skillDto = await _mediator.Send(command);
            if (skillDto == null)
            {
                return ResponseNotFound("Skill not found or has been deleted");
            }
            return ResponseOk(skillDto, "Skill updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteSkillCommand { Id = id };
            var result = await _mediator.Send(command);
            if (!result)
            {
                return ResponseNotFound("Skill not found or already deleted");
            }
            return ResponseNoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetSkillQuery { Id = id };
            var skillDto = await _mediator.Send(query);
            if (skillDto == null)
            {
                return ResponseNotFound("Skill not found or has been deleted");
            }
            return ResponseOk(skillDto, "Skill retrieved successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetSkillsListQuery request)
        {

            var response = await _mediator.Send(request);
            return ResponseOk(response.Data, response.Paging, "Skills retrieved successfully");
        }


    }
}
