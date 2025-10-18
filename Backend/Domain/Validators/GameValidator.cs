using Core.Domain.Validators;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    public class GameValidator : EntityValidator<Game>
    {
        public GameValidator()
        {
            RuleFor(game => game.GameId).NotNull().NotEmpty().WithMessage("Game ID must not be null or empty.");
            RuleFor(game => game.PlayerId).NotNull().NotEmpty().WithMessage("Player ID must not be null or empty.");
            RuleFor(game => game.SecretNumber).NotNull().NotEmpty().WithMessage("Secret Number must not be null or empty.");
            RuleFor(game => game.CreatedAt).NotNull().WithMessage("Created At must not be null.");
            RuleFor(game => game.Status).IsInEnum().WithMessage("Status must be a valid enum value.");
        }
    }
}
