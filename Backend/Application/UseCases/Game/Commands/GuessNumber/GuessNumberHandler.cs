using Application.DataTransferObjects;
using Application.Repositories;
using Core.Application;
using Domain.Entities;
using GameCore;
using Microsoft.Extensions.Logging;
using Application.Exceptions;

namespace Application.UseCases.Game.Commands.GuessNumber
{
    /// <summary>
    /// Handler para el comando de intento de adivinanza
    /// </summary>
    public class GuessNumberHandler : IRequestCommandHandler<GuessNumberCommand, GuessNumberResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IAttemptRepository _attemptRepository;
        private readonly ILogger<GuessNumberHandler> _logger;

        public GuessNumberHandler(
            IGameRepository gameRepository,
            IAttemptRepository attemptRepository,
            ILogger<GuessNumberHandler> logger)
        {
            _gameRepository = gameRepository;
            _attemptRepository = attemptRepository;
            _logger = logger;
        }

        public async Task<GuessNumberResponse> Handle(GuessNumberCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Procesando intento para GameId: {GameId}, Número: {AttemptedNumber}",
                request.GameId, request.AttemptedNumber);

            // Validar que el número tenga 4 dígitos
            if (string.IsNullOrWhiteSpace(request.AttemptedNumber) || request.AttemptedNumber.Length != 4)
                throw new BadRequestException("El número debe tener exactamente 4 dígitos");

            // Validar que sean solo dígitos
            if (!request.AttemptedNumber.All(char.IsDigit))
                throw new BadRequestException("El número debe contener solo dígitos");

            // Validar que no haya dígitos repetidos
            if (request.AttemptedNumber.Distinct().Count() != 4)
                throw new BadRequestException("El número no debe tener dígitos repetidos");

            // Obtener el juego
            var game = await _gameRepository.GetByIdWithAttemptsAsync(request.GameId);
            if (game == null)
            {
                _logger.LogWarning("Intento de jugar en juego inexistente: {GameId}", request.GameId);
                throw new NotFoundException($"El juego con ID {request.GameId} no existe");
            }

            // Verificar que el juego esté activo
            if (!game.IsActive())
            {
                _logger.LogWarning("Intento de jugar en juego finalizado: {GameId}", request.GameId);
                throw new BadRequestException($"El juego {request.GameId} ya ha finalizado.");
            }

            // Convertir números a enteros para usar la librería GameCore
            string secretNumber = game.SecretNumber;
            string attemptedNumber = request.AttemptedNumber;

            // Evaluar el intento usando la librería ESCMB.GameCore
            var evaluationResult = Evaluator.ValidateAttempt(secretNumber, attemptedNumber);

            _logger.LogInformation("Evaluación - Famas: {Famas}, Picas: {Picas}, Mensaje: {Message}",
                evaluationResult.Fama, evaluationResult.Pica, evaluationResult.Message);

            // Registrar el intento en la base de datos
            var attempt = new Attempt
            {
                GameId = request.GameId,
                AttemptedNumber = request.AttemptedNumber,
                Famas = evaluationResult.Fama,
                Picas = evaluationResult.Pica,
                Message = evaluationResult.Message,
                AttemptDate = DateTime.Now
            };

            await _attemptRepository.AddAsync(attempt);

            // Si adivinó el número (4 famas), marcar el juego como finalizado
            if (evaluationResult.Fama == 4)
            {
                game.MarkAsFinished();
                _gameRepository.Update(game.GameId, game);
                _logger.LogInformation("¡Juego {GameId} completado exitosamente!", request.GameId);
            }

            _logger.LogInformation("Intento registrado exitosamente para GameId: {GameId}", request.GameId);

            return new GuessNumberResponse
            {
                GameId = request.GameId,
                AttemptedNumber = request.AttemptedNumber,
                Message = evaluationResult.Message,
                Famas = evaluationResult.Fama,
                Picas = evaluationResult.Pica
            };
        }
    }
}
