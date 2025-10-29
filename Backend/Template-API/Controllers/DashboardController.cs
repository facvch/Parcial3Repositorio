using Application.DataTransferObjects;
using Application.UseCases.Dashboard.Queries.GetDashboardStats;
using Application.UseCases.Dashboard.Queries.GetUsersPerDay;
using Application.UseCases.Dashboard.Queries.GetTop5Games;
using Application.UseCases.Dashboard.Queries.GetAllGamesWithAttempts;
using Core.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// Controlador para datos del dashboard
    /// </summary>
    [ApiController]
    [Route("api/dashboard/v1")]
    public class DashboardController : ControllerBase 
    {
        private readonly ICommandQueryBus _bus;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ICommandQueryBus bus, ILogger<DashboardController> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene estadísticas generales del dashboard
        /// </summary>
        [HttpGet("stats")]
        [ProducesResponseType(typeof(DashboardStatsDto), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                _logger.LogInformation("GET /api/dashboard/v1/stats");

                var query = new GetDashboardStatsQuery();
                var response = await _bus.Send(query);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener stats");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtiene usuarios registrados por día
        /// </summary>
        [HttpGet("users-per-day")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUsersPerDay([FromQuery] int days = 7)
        {
            try
            {
                _logger.LogInformation("GET /api/dashboard/v1/users-per-day?days={Days}", days);

                var query = new GetUsersPerDayQuery { Days = days };
                var response = await _bus.Send(query);

                return Ok(response.Items.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener usuarios por día");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtiene top 5 juegos con menos intentos
        /// </summary>
        [HttpGet("top5-games")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetTop5Games()
        {
            try
            {
                _logger.LogInformation("GET /api/dashboard/v1/top5-games");

                var query = new GetTop5GamesQuery();
                var response = await _bus.Send(query);

                return Ok(response.Items.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener top 5 juegos");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        /// <summary>
        /// Obtiene todos los juegos con sus intentos
        /// </summary>
        [HttpGet("games-attempts")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetGamesAttempts()
        {
            try
            {
                _logger.LogInformation("GET /api/dashboard/v1/games-attempts");

                var query = new GetAllGamesWithAttemptsQuery();
                var response = await _bus.Send(query);

                return Ok(response.Items.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener juegos con intentos");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}
