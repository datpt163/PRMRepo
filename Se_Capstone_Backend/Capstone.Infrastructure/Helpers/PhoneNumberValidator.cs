using System.Text.RegularExpressions;

namespace Capstone.Domain.Helpers
{
    public static class PhoneNumberValidator
    {
        public static bool Validate(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d+(-\d+)*$");
        }
    }

}
