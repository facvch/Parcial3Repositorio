using Application.DataTransferObjects;
using Application.Repositories;
using Core.Application;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Dashboard.Queries.GetTop5Games
{
    internal class GetTop5GamesHandler : IRequestQueryHandler<GetTop5GamesQuery, QueryResult<TopGameDto>>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAttemptRepository _attemptRepository;

        public GetTop5GamesHandler(
            IGameRepository gameRepository,
            IPlayerRepository playerRepository,
            IAttemptRepository attemptRepository)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            _attemptRepository = attemptRepository ?? throw new ArgumentNullException(nameof(attemptRepository));
        }

        public async Task<QueryResult<TopGameDto>> Handle(GetTop5GamesQuery request, CancellationToken cancellationToken)
        {
            var allGames = await _gameRepository.FindAllAsync();
            var allPlayers = await _playerRepository.FindAllAsync();
            var allAttempts = await _attemptRepository.FindAllAsync();

            var finishedGames = allGames
                .Where(g => g.Status == Domain.Enums.Enums.GameStatus.Finished)
                .Select(g =>
                {
                    var player = allPlayers.FirstOrDefault(p => p.PlayerId == g.PlayerId);
                    var attemptsCount = allAttempts.Count(a => a.GameId == g.GameId);

                    return new TopGameDto
                    {
                        GameId = g.GameId,
                        PlayerId = g.PlayerId,
                        PlayerName = player != null ? $"{player.FirstName} {player.LastName}" : "Desconocido",
                        Attempts = attemptsCount,
                        CreatedAt = g.CreatedAt
                    };
                })
                .OrderBy(g => g.Attempts)
                .Take(5)
                .ToList();

            return new QueryResult<TopGameDto>(finishedGames, finishedGames.Count, request.PageIndex, request.PageSize);
        }
    }
}