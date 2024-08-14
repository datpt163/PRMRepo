using Capstone.Application.Module.Account.Query;
using FluentValidation;

namespace Capstone.Api.Module.Account.Validator
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordQuery>
    {
        public ForgotPasswordValidator() 
        {
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
