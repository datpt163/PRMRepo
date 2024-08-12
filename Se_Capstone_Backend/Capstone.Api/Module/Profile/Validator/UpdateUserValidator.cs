using Capstone.Api.Module.Profile.Request;
using FluentValidation;

namespace Capstone.Api.Module.Profile.Validator
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}

