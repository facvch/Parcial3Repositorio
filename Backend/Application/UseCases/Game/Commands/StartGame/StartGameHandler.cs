using Application.DataTransferObjects;
using Application.Repositories;
using Core.Application;
using Microsoft.Extensions.Logging;
using Application.Exceptions;

namespace Application.UseCases.Game.Commands.StartGame
{
    /// <summary>
    /// Handler para el comando de inicio de juego
    /// </summary>
    public class StartGameHandler : IRequestCommandHandler<StartGameCommand, StartGameResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<StartGameHandler> _logger;

        public StartGameHandler(
            IGameRepository gameRepository,
            IPlayerRepository playerRepository,
            ILogger<StartGameHandler> logger)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _logger = logger;
        }

        public async Task<StartGameResponse> Handle(StartGameCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando nuevo juego para PlayerId: {PlayerId}", request.PlayerId);

            // Validar que el PlayerId sea válido
            if (request.PlayerId <= 0)
                throw new BadRequestException("El ID del jugador debe ser mayor a 0");

            // Verificar que el jugador exista
            var player = await _playerRepository.GetByIdAsync(request.PlayerId);
            if (player == null)
            {
                _logger.LogWarning("Intento de iniciar juego con jugador no encontrado: {PlayerId}", request.PlayerId);
                throw new NotFoundException($"El jugador con ID {request.PlayerId} no existe");
            }

            // Verificar si el jugador tiene un juego activo
            var activeGame = await _gameRepository.GetActiveGameByPlayerIdAsync(request.PlayerId);
            if (activeGame != null)
            {
                _logger.LogWarning("Jugador {PlayerId} tiene un juego activo: {GameId}",
                    request.PlayerId, activeGame.GameId);
                throw new BadRequestException($"Ya tienes un juego activo (ID: {activeGame.GameId}). Debes finalizarlo antes de iniciar uno nuevo.");
            }

            // Generar número secreto de 4 dígitos sin repetir
            var secretNumber = GenerateSecretNumber();
            _logger.LogInformation("Número secreto generado para juego (no mostrar en producción): {SecretNumber}", secretNumber);

            // Crear nuevo juego
            var game = new Domain.Entities.Game
            {
                PlayerId = request.PlayerId,
                SecretNumber = secretNumber,
                CreatedAt = DateTime.Now,
                Status = Domain.Enums.Enums.GameStatus.Active
            };

            await _gameRepository.AddAsync(game);

            _logger.LogInformation("Juego creado exitosamente con ID: {GameId} para PlayerId: {PlayerId}",
                game.GameId, request.PlayerId);

            return new StartGameResponse
            {
                GameId = game.GameId,
                PlayerId = request.PlayerId,
                CreatedAt = game.CreatedAt
            };
        }

        /// <summary>
        /// Genera un número aleatorio de 4 dígitos sin dígitos repetidos
        /// </summary>
        private string GenerateSecretNumber()
        {
            var random = new Random();
            var digits = Enumerable.Range(0, 10).OrderBy(x => random.Next()).Take(4).ToList();
            return string.Join("", digits);
        }
    }
}
