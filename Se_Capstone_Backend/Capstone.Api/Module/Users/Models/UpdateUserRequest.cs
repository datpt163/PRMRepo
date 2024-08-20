namespace Capstone.Api.Module.Users.Models
{
    public class UpdateUserRequest
    {
        public Guid? Id { get; set; }
        public string? FullName { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Avatar { get; set; } = string.Empty;
        public IFormFile? AvatarFile { get; set; }
    }
}
