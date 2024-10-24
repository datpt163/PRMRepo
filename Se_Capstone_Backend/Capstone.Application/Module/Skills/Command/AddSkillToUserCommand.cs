﻿using MediatR;


namespace Capstone.Application.Module.Skills.Command
{
    public class AddSkillToUserCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid SkillId { get; set; }
    }

}
