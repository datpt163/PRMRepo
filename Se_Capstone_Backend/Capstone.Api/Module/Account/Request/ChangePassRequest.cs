namespace Capstone.Api.Module.Account.Request
{
    public class ChangePassRequest
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
