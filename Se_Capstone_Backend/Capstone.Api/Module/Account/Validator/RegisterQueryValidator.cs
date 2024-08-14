using Capstone.Application.Module.Account.Query;
using FluentValidation;

namespace Capstone.Api.Module.Account.Validator
{
    public class RegisterQueryValidator : AbstractValidator<RegisterQuery>
    {
        public RegisterQueryValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Phone)
           .NotEmpty()
           .Matches(@"^\d{8,15}$")
           .WithMessage("Phone number must have 8 to 15 number");
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Avatar).NotEmpty();
        }
    }
}
