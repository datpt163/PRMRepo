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
          .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be between 10 and 15 digits, and may start with a '+'.");
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
            RuleFor(x => x.Dob)
            .Must(BeValidDateWithoutTimeZone)
            .WithMessage("Date of birth must not include time zone information.");
        }

        private bool BeValidDateWithoutTimeZone(DateTime dob)
        {
            return dob.Kind == DateTimeKind.Unspecified;
        }
    }
}
