using Application.DataTransferObjects;
using Core.Application;

namespace Application.UseCases.Player.Commands.RegisterPlayer
{
    /// <summary>
    /// Comando para registrar un nuevo jugador
    /// </summary>
    public class RegisterPlayerCommand : IRequestCommand<RegisterPlayerResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}