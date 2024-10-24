namespace Capstone.Api.Module.Skills.Request
{
    public class RemoveSkillFromUserRequest
    {
        public Guid UserId { get; set; }
        public Guid SkillId { get; set; }
    }

}
