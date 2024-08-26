namespace Capstone.Api.Module.Auths.Request
{
    public class UpdateUserRequest
    {
        public string? FullName { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Avatar { get; set; } = string.Empty;
        public IFormFile? AvatarFile { get; set; }
        public string? Address { get; set; } = string.Empty;
        public int? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? BankAccount { get; set; } = string.Empty;
        public string? BankAccountName { get; set; } = string.Empty;
    }
}
