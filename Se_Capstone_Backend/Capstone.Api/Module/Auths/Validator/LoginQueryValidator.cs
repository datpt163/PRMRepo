using Capstone.Application.Module.Auth.Query;
using FluentValidation;

namespace Capstone.Api.Module.Auth.Validator
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Passwords must be at least 6 characters.")
            .Matches(@"[A-Z]").WithMessage("Passwords must have at least one uppercase ('A'-'Z').")
            .Matches(@"[a-z]").WithMessage("Passwords must have at least one lowercase ('a'-'z').")
            .Matches(@"[0-9]").WithMessage("Passwords must have at least one digit ('0'-'9').")
            .Matches(@"[\W_]").WithMessage("Passwords must have at least one non alphanumeric character.");
        }
    }
}
