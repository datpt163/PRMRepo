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
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
          .MinimumLength(6).WithMessage("Passwords must be at least 6 characters.")
          .Matches(@"[A-Z]").WithMessage("Passwords must have at least one uppercase ('A'-'Z').")
          .Matches(@"[a-z]").WithMessage("Passwords must have at least one lowercase ('a'-'z').")
          .Matches(@"[0-9]").WithMessage("Passwords must have at least one digit ('0'-'9').")
          .Matches(@"[\W_]").WithMessage("Passwords must have at least one non alphanumeric character.");
        }
    }
}
