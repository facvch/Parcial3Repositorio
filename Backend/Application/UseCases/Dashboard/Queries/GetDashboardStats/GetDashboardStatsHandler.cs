using Application.DataTransferObjects;
using Application.Repositories;
using Core.Application;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Dashboard.Queries.GetDashboardStats
{
    internal class GetDashboardStatsHandler : IRequestQueryHandler<GetDashboardStatsQuery, DashboardStatsDto>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAttemptRepository _attemptRepository;
        private readonly ILogger<GetDashboardStatsHandler> _logger;

        public GetDashboardStatsHandler(
            IGameRepository gameRepository,
            IPlayerRepository playerRepository,
            IAttemptRepository attemptRepository,
            ILogger<GetDashboardStatsHandler> logger)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            _attemptRepository = attemptRepository ?? throw new ArgumentNullException(nameof(attemptRepository));
            _logger = logger;
        }

        public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Obteniendo estadísticas del dashboard");

            var allPlayers = await _playerRepository.FindAllAsync();
            var allGames = await _gameRepository.FindAllAsync();
            var allAttempts = await _attemptRepository.FindAllAsync();

            var totalPlayers = allPlayers.Count;
            var totalGames = allGames.Count;
            var gamesFinished = allGames.Count(g => g.Status == Domain.Enums.Enums.GameStatus.Finished);

            var finishedGames = allGames.Where(g => g.Status == Domain.Enums.Enums.GameStatus.Finished).ToList();
            var averageAttempts = 0.0;

            if (finishedGames.Any())
            {
                var attemptsPerGame = finishedGames.Select(g =>
                    allAttempts.Count(a => a.GameId == g.GameId)
                ).ToList();

                averageAttempts = attemptsPerGame.Any() ? attemptsPerGame.Average() : 0;
            }

            return new DashboardStatsDto
            {
                TotalPlayers = totalPlayers,
                TotalGames = totalGames,
                GamesFinished = gamesFinished,
                AverageAttempts = Math.Round(averageAttempts, 2)
            };
        }
    }
}