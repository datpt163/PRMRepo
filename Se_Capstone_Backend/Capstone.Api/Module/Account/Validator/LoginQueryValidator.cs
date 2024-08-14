using Capstone.Application.Module.Account.Query;
using FluentValidation;

namespace Capstone.Api.Module.Account.Validator
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
