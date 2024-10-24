﻿namespace Capstone.Api.Module.Skills.Request
{
    public class RemoveSkillsFromUserRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> SkillIds { get; set; } = new List<Guid>();
    }

}
