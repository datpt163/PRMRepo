using Capstone.Application.Module.Auth.Query;
using FluentValidation;

namespace Capstone.Api.Module.Auth.Validator
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
