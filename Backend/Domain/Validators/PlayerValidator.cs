using Core.Domain.Validators;
using FluentValidation;

namespace Domain.Validators
{
    public class PlayerValidator : EntityValidator<string>
    {
        public PlayerValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage("Player ID must not be null or empty.");
        }
    }
}
