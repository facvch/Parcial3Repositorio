using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Infraestructure.Repositories.Sql;

namespace Infrastructure.Repositories.Sql
{
    /// <summary>
    /// Implementación del repositorio de jugadores para SQL
    /// </summary>
    internal class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        private readonly StoreDbContext _context;

        public PlayerRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Player> GetByNameAsync(string firstName, string lastName)
        {
            return await _context.Players
                .FirstOrDefaultAsync(p => p.FirstName == firstName && p.LastName == lastName);
        }

        public async Task<bool> ExistsAsync(string firstName, string lastName)
        {
            return await _context.Players
                .AnyAsync(p => p.FirstName == firstName && p.LastName == lastName);
        }

        public async Task<Player> GetByIdAsync(int playerId)
        {
            return await _context.Players
                .FirstOrDefaultAsync(p => p.PlayerId == playerId);
        }
    }

}