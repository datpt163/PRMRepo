namespace Capstone.Api.Module.Account.Request
{
    public class ChangePassRequest
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
