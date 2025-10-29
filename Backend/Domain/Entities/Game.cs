using Core.Domain.Entities;
using Domain.Validators;
using static Domain.Enums.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad de dominio que representa un juego de Picas y Famas
    /// </summary>
    public class Game : DomainEntity<string, GameValidator>
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public string SecretNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public GameStatus Status { get; set; }

        public virtual Player Player { get; set; }
        public virtual ICollection<Attempt> Attempts { get; set; }

        public Game()
        {
            Attempts = new List<Attempt>();
            CreatedAt = DateTime.Now;
            Status = GameStatus.Active;
        }

        /// <summary>
        /// Marca el juego como finalizado
        /// </summary>
        public void MarkAsFinished()
        {
            Status = GameStatus.Finished;
        }

        /// <summary>
        /// Verifica si el juego está activo
        /// </summary>
        public bool IsActive()
        {
            return Status == GameStatus.Active;
        }
    }
}