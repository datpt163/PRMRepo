namespace Capstone.Application.Common.Gpt
{
    public class ChatGptResponse
    {
        public Choice[] Choices { get; set; } = new Choice[0];

        public class Choice
        {
            public ChoiceMessage Message { get; set; }  = new ChoiceMessage();

            public class ChoiceMessage
            {
                public string Role { get; set; } = string.Empty;
                public string Content { get; set; } = string.Empty;
            }
        }
    }
}
