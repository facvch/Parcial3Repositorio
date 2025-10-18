using Core.Application.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    /// <summary>
    /// Interfaz del repositorio de jugadores
    /// </summary>
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<Player> GetByNameAsync(string firstName, string lastName);
        Task<bool> ExistsAsync(string firstName, string lastName);
        Task<Player> GetByIdAsync(int playerId);
    }
}