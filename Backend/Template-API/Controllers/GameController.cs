using Application.DataTransferObjects;
using Application.UseCases.Game.Commands.GuessNumber;
using Application.UseCases.Game.Commands.StartGame;
using Application.UseCases.Player.Commands.RegisterPlayer;
using Controllers;
using Core.Application;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controlador para el juego de Picas y Famas
    /// </summary>
    [ApiController]
    [Route("api/game/v1")]
    public class GameController : BaseController
    {
        private readonly ICommandQueryBus _bus;
        private readonly ILogger<GameController> _logger;

        public GameController(ICommandQueryBus bus, ILogger<GameController> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        /// <summary>
        /// Registra un nuevo jugador en el sistema
        /// </summary>
        /// <param name="request">Datos del jugador (firstName, lastName, age)</param>
        /// <returns>ID del jugador registrado</returns>
        /// <response code="200">Jugador registrado exitosamente</response>
        /// <response code="400">Datos inválidos o jugador ya existe</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterPlayerResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> Register([FromBody] RegisterPlayerRequest request)
        {
            try
            {
                _logger.LogInformation("POST /api/game/v1/register - Registrando jugador: {FirstName} {LastName}",
                    request?.FirstName, request?.LastName);

                var command = new RegisterPlayerCommand
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Age = request.Age
                };

                var response = await _bus.Send(command);

                _logger.LogInformation("Jugador registrado exitosamente con ID: {PlayerId}", response.PlayerId);

                return Ok(response);
            }
            catch (Application.Exceptions.BadRequestException ex)
            {
                _logger.LogWarning("Error de validación en registro: {Message}", ex.Message);
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno al registrar jugador");
                return StatusCode(500, new ErrorResponse { Message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Inicia un nuevo juego para un jugador registrado
        /// </summary>
        /// <param name="request">ID del jugador</param>
        /// <returns>Información del juego creado (gameId, playerId, createdAt)</returns>
        /// <response code="200">Juego iniciado exitosamente</response>
        /// <response code="400">PlayerId inválido o jugador tiene juego activo</response>
        /// <response code="404">Jugador no encontrado</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost("start")]
        [ProducesResponseType(typeof(StartGameResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> Start([FromBody] StartGameRequest request)
        {
            try
            {
                _logger.LogInformation("POST /api/game/v1/start - Iniciando juego para PlayerId: {PlayerId}",
                    request?.PlayerId);

                var command = new StartGameCommand
                {
                    PlayerId = request.PlayerId
                };

                var response = await _bus.Send(command);

                _logger.LogInformation("Juego iniciado exitosamente - GameId: {GameId}", response.GameId);

                return Ok(response);
            }
            catch (Application.Exceptions.BadRequestException ex)
            {
                _logger.LogWarning("Error de validación al iniciar juego: {Message}", ex.Message);
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (Application.Exceptions.NotFoundException ex)
            {
                _logger.LogWarning("Jugador no encontrado: {Message}", ex.Message);
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno al iniciar juego");
                return StatusCode(500, new ErrorResponse { Message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Intenta adivinar el número secreto
        /// </summary>
        /// <param name="request">ID del juego y número de 4 dígitos sin repetir</param>
        /// <returns>Resultado del intento con pistas (famas y picas)</returns>
        /// <response code="200">Intento procesado exitosamente</response>
        /// <response code="400">Número inválido o juego finalizado</response>
        /// <response code="404">Juego no encontrado</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost("guess")]
        [ProducesResponseType(typeof(GuessNumberResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<IActionResult> Guess([FromBody] GuessNumberRequest request)
        {
            try
            {
                _logger.LogInformation("POST /api/game/v1/guess - GameId: {GameId}, Número: {AttemptedNumber}",
                    request?.GameId, request?.AttemptedNumber);

                var command = new GuessNumberCommand
                {
                    GameId = request.GameId,
                    AttemptedNumber = request.AttemptedNumber
                };

                var response = await _bus.Send(command);

                _logger.LogInformation("Intento procesado - Famas: {Famas}, Picas: {Picas}",
                    response.Famas, response.Picas);

                return Ok(response);
            }
            catch (Application.Exceptions.BadRequestException ex)
            {
                _logger.LogWarning("Error de validación en intento: {Message}", ex.Message);
                return BadRequest(new ErrorResponse { Message = ex.Message });
            }
            catch (Application.Exceptions.NotFoundException ex)
            {
                _logger.LogWarning("Juego no encontrado: {Message}", ex.Message);
                return NotFound(new ErrorResponse { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error interno al procesar intento");
                return StatusCode(500, new ErrorResponse { Message = "Error interno del servidor" });
            }
        }
    }
}