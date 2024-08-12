using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Account.Model
{
    public class RegisterRedisData
    {
        public int Otp { get; set; }
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public RegisterRedisData(int otp, string password, string firstName, string lastName)
        {
            Otp = otp;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
