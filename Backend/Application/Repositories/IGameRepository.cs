using Core.Application.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    /// <summary>
    /// Interfaz del repositorio de juegos
    /// </summary>
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game> GetActiveGameByPlayerIdAsync(int playerId);
        Task<Game> GetByIdWithAttemptsAsync(int gameId);
    }
}