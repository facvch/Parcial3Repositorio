using Application.DataTransferObjects;
using Core.Application;

namespace Application.UseCases.Game.Commands.StartGame
{
    /// <summary>
    /// Comando para iniciar un nuevo juego
    /// </summary>
    public class StartGameCommand : IRequestCommand<StartGameResponse>
    {
        public int PlayerId { get; set; }
    }
}