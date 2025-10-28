using Application.DataTransferObjects;
using Application.Repositories;
using Core.Application;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Dashboard.Queries.GetAllGamesWithAttempts
{
    internal class GetAllGamesWithAttemptsHandler : IRequestQueryHandler<GetAllGamesWithAttemptsQuery, QueryResult<GameAttemptsDto>>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAttemptRepository _attemptRepository;

        public GetAllGamesWithAttemptsHandler(
            IGameRepository gameRepository,
            IPlayerRepository playerRepository,
            IAttemptRepository attemptRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            _attemptRepository = attemptRepository ?? throw new ArgumentNullException(nameof(attemptRepository));
        }

        public async Task<QueryResult<GameAttemptsDto>> Handle(GetAllGamesWithAttemptsQuery request, CancellationToken cancellationToken)
        {
            var allGames = await _gameRepository.FindAllAsync();
            var allPlayers = await _playerRepository.FindAllAsync();
            var allAttempts = await _attemptRepository.FindAllAsync();

            var gamesWithAttempts = allGames
                .Select(g =>
                {
                    var player = allPlayers.FirstOrDefault(p => p.PlayerId == g.PlayerId);
                    var attemptsCount = allAttempts.Count(a => a.GameId == g.GameId);

                    return new GameAttemptsDto
                    {
                        GameId = g.GameId,
                        PlayerId = g.PlayerId,
                        PlayerName = player != null ? $"{player.FirstName} {player.LastName}" : "Desconocido",
                        Attempts = attemptsCount,
                        Status = g.Status == Domain.Enums.Enums.GameStatus.Finished ? "Finalizado" : "Activo",
                        CreatedAt = g.CreatedAt
                    };
                })
                .OrderByDescending(g => g.CreatedAt)
                .ToList();

            return new QueryResult<GameAttemptsDto>(gamesWithAttempts, gamesWithAttempts.Count, request.PageIndex, request.PageSize);
        }
    }
}