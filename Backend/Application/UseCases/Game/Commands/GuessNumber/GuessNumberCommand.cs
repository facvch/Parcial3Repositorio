using Application.DataTransferObjects;
using Core.Application;

namespace Application.UseCases.Game.Commands.GuessNumber
{
    /// <summary>
    /// Comando para intentar adivinar el número secreto
    /// </summary>
    public class GuessNumberCommand : IRequestCommand<GuessNumberResponse>
    {
        public int GameId { get; set; }
        public string AttemptedNumber { get; set; }
    }
}