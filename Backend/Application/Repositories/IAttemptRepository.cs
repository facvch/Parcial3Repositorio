using Core.Application.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    /// <summary>
    /// Interfaz del repositorio de intentos
    /// </summary>
    public interface IAttemptRepository : IRepository<Attempt>
    {
        Task<int> GetAttemptCountByGameIdAsync(int gameId);
    }
}