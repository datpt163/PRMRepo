

namespace Capstone.Application.Localization
{
    public class MessageLocalizer
    {
        private readonly Dictionary<string, string> _messages;

        public MessageLocalizer()
        {
            _messages = new Dictionary<string, string>
            {
                { "ApplicantCreated", "Applicant created successfully." },
                { "ApplicantNotFound", "Applicant not found." },
                { "ApplicantDeleted", "Applicant deleted successfully." },
                { "ApplicantUpdated", "Applicant updated successfully." },
            };
        }

        public string GetMessage(string key)
        {
            return _messages.TryGetValue(key, out var message) ? message : "Message not found.";
        }
    }
}
