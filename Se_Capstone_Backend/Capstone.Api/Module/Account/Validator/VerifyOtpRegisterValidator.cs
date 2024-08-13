using Capstone.Application.Module.Account.Query;
using FluentValidation;

namespace Capstone.Api.Module.Account.Validator
{
    public class VerifyOtpRegisterValidator : AbstractValidator<VerifyOtpRegisterQuery>
    {
        public VerifyOtpRegisterValidator() 
        {
            RuleFor(x => x.Otp)
            .GreaterThan(99999).WithMessage("Otp must have 6 digits")
            .LessThanOrEqualTo(999999).WithMessage("Otp must have 6 digits");
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
