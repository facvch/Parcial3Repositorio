using Core.Domain.Validators;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class AttemptValidator : EntityValidator<Attempt>
    {
        public AttemptValidator()
        {
            RuleFor(attempt => attempt.AttemptId).NotNull().NotEmpty().WithMessage(DomainConstants.NOTNULL_OR_EMPTY);
            RuleFor(attempt => attempt.GameId).NotNull().NotEmpty().WithMessage(DomainConstants.NOTNULL_OR_EMPTY);
            RuleFor(attempt => attempt.AttemptedNumber).NotNull().NotEmpty().WithMessage(DomainConstants.NOTNULL_OR_EMPTY);
            RuleFor(attempt => attempt.Famas).GreaterThanOrEqualTo(0).WithMessage("Famas must be greater than or equal to 0.");
            RuleFor(attempt => attempt.Picas).GreaterThanOrEqualTo(0).WithMessage("Picas must be greater than or equal to 0.");
            RuleFor(attempt => attempt.AttemptDate).NotNull().WithMessage(DomainConstants.NOTNULL);
        }
    }
}
