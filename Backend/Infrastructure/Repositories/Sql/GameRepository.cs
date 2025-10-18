using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Infraestructure.Repositories.Sql;
using static Domain.Enums.Enums;

namespace Infrastructure.Repositories.Sql
{
    /// <summary>
    /// Implementación del repositorio de juegos para SQL
    /// </summary>
    internal class GameRepository : BaseRepository<Game>, IGameRepository
    {
        private readonly StoreDbContext _context;

        public GameRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Game> GetActiveGameByPlayerIdAsync(int playerId)
        {
            return await _context.Games
                .FirstOrDefaultAsync(g => g.PlayerId == playerId && g.Status == GameStatus.Active);
        }

        public async Task<Game> GetByIdWithAttemptsAsync(int gameId)
        {
            return await _context.Games
                .Include(g => g.Attempts)
                .FirstOrDefaultAsync(g => g.GameId == gameId);
        }
    }
}