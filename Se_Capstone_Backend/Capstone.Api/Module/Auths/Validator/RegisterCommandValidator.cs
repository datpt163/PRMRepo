using Capstone.Application.Module.Auth.Command;
using Capstone.Domain.Enums;
using FluentValidation;

namespace Capstone.Api.Module.Auth.Validator
{
    public class RegisterCommandValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Phone)
           .NotEmpty()
           .Matches(@"^\d{8,15}$")
           .WithMessage("Phone number must have 8 to 15 number");
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Gender)
             .IsInEnum();
        }
    }
}
