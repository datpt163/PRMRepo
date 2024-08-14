using Capstone.Application.Module.Account.Command;
using FluentValidation;

namespace Capstone.Api.Module.Account.Validator
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator() 
        {
            RuleFor(s => s.Code).NotEmpty();
            RuleFor(s =>s.NewPassword).NotEmpty();
        }
    }
}
