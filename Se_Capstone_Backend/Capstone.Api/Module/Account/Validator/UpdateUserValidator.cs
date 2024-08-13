using Capstone.Api.Module.Account.Request;
using FluentValidation;

namespace Capstone.Api.Module.Account.Validator
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Phone)
           .NotEmpty()
           .Matches(@"^\d{8,15}$")
           .WithMessage("Phone number must have 8 to 15 number");
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Avatar).NotEmpty();
        }
    }
}

