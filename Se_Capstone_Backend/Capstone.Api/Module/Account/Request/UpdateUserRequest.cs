namespace Capstone.Api.Module.Account.Request
{
    public class UpdateUserRequest
    {
        public string FullName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Avatar { get; set; } = null!;
    }
}
