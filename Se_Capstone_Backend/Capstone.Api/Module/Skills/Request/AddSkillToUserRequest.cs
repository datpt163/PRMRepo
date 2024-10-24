namespace Capstone.Api.Module.Skills.Request
{
    public class AddSkillToUserRequest
    {
        public Guid UserId { get; set; }
        public Guid SkillId { get; set; }
    }

}
