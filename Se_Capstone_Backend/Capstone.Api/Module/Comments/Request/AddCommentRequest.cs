namespace Capstone.Api.Module.Comments.Request
{
    public class AddCommentRequest
    {
        public Guid IssueId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
