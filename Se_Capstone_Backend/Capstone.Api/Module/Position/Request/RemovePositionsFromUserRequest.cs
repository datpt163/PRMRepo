namespace Capstone.Api.Module.Positions.Request
{
    public class RemovePositionsFromUserRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> PositionIds { get; set; } = new List<Guid>();
    }

}
