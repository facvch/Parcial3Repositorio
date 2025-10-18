using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Infraestructure.Repositories.Sql;

namespace Infrastructure.Repositories.Sql
{
    /// <summary>
    /// Implementación del repositorio de intentos para SQL
    /// </summary>
    internal class AttemptRepository : BaseRepository<Attempt>, IAttemptRepository
    {
        private readonly StoreDbContext _context;

        public AttemptRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetAttemptCountByGameIdAsync(int gameId)
        {
            return await _context.Attempts
                .Where(a => a.GameId == gameId)
                .CountAsync();
        }
    }
}