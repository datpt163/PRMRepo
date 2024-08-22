using Capstone.Api.Module.Auths.Request;
using FluentValidation;

namespace Capstone.Api.Module.Auths.Validator
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Password is required.")
          .MinimumLength(6).WithMessage("Passwords must be at least 6 characters.")
          .Matches(@"[A-Z]").WithMessage("Passwords must have at least one uppercase ('A'-'Z').")
          .Matches(@"[a-z]").WithMessage("Passwords must have at least one lowercase ('a'-'z').")
          .Matches(@"[0-9]").WithMessage("Passwords must have at least one digit ('0'-'9').")
          .Matches(@"[\W_]").WithMessage("Passwords must have at least one non alphanumeric character.");

            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Password is required.")
       .MinimumLength(6).WithMessage("Passwords must be at least 6 characters.")
       .Matches(@"[A-Z]").WithMessage("Passwords must have at least one uppercase ('A'-'Z').")
       .Matches(@"[a-z]").WithMessage("Passwords must have at least one lowercase ('a'-'z').")
       .Matches(@"[0-9]").WithMessage("Passwords must have at least one digit ('0'-'9').")
       .Matches(@"[\W_]").WithMessage("Passwords must have at least one non alphanumeric character.");
        }
    }
}
