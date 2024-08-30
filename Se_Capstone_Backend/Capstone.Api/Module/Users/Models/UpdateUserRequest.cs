namespace Capstone.Api.Module.Users.Models
{
    public class UpdateUserRequest
    {
        public Guid? Id { get; set; }
        public string? FullName { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public IFormFile? Avatar { get; set; }
        public string? Address { get; set; } = string.Empty;
        public int? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? BankAccount { get; set; } = string.Empty;
        public string? BankAccountName { get; set; } = string.Empty;
        public Guid? RoleId { get; set; }
        public int? Status { get; set; }
    }
}
