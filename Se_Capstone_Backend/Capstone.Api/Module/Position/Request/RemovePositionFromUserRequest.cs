namespace Capstone.Api.Module.Positions.Request
{
    public class RemovePositionFromUserRequest
    {
        public Guid UserId { get; set; }
        public Guid PositionId { get; set; }
    }

}
