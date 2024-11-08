namespace Capstone.Api.Module.Positions.Request
{
    public class AddPositionToUserRequest
    {
        public Guid UserId { get; set; }
        public Guid PositionId { get; set; }
    }

}
