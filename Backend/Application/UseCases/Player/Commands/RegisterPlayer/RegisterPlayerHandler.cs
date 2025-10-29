using Application.DataTransferObjects;
using Application.Repositories;
using Core.Application;
using Microsoft.Extensions.Logging;
using Application.Exceptions;

namespace Application.UseCases.Player.Commands.RegisterPlayer
{
    /// <summary>
    /// Handler para el comando de registro de jugador
    /// </summary>
    public class RegisterPlayerHandler : IRequestCommandHandler<RegisterPlayerCommand, RegisterPlayerResponse>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<RegisterPlayerHandler> _logger;

        public RegisterPlayerHandler(
            IPlayerRepository playerRepository,
            ILogger<RegisterPlayerHandler> logger)
        {
            _playerRepository = playerRepository;
            _logger = logger;
        }

        public async Task<RegisterPlayerResponse> Handle(RegisterPlayerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando registro de jugador: {FirstName} {LastName}",
                request.FirstName, request.LastName);

            if (string.IsNullOrWhiteSpace(request.FirstName))
                throw new BadRequestException("El nombre es requerido");

            if (string.IsNullOrWhiteSpace(request.LastName))
                throw new BadRequestException("El apellido es requerido");

            if (request.Age <= 0)
                throw new BadRequestException("La edad debe ser mayor a 0");

            var existingPlayer = await _playerRepository.GetByNameAsync(request.FirstName, request.LastName);
            if (existingPlayer != null)
            {
                _logger.LogWarning("Intento de registro duplicado: {FirstName} {LastName}",
                    request.FirstName, request.LastName);
                throw new BadRequestException("El jugador ya se encuentra registrado");
            }

            var player = new Domain.Entities.Player
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Age = request.Age,
                RegistrationDate = DateTime.Now
            };

            await _playerRepository.AddAsync(player);

            _logger.LogInformation("Jugador registrado exitosamente con ID: {PlayerId}", player.PlayerId);

            return new RegisterPlayerResponse
            {
                PlayerId = player.PlayerId
            };
        }
    }
}
