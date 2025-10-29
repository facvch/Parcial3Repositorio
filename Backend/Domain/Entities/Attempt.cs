using Core.Domain.Entities;
using Domain.Validators;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad de dominio que representa un intento de adivinanza
    /// </summary>
    public class Attempt : DomainEntity<string, AttemptValidator>
    {
        public int AttemptId { get; set; }
        public int GameId { get; set; }
        public string AttemptedNumber { get; set; }
        public int Famas { get; set; }
        public int Picas { get; set; }
        public string Message { get; set; }
        public DateTime AttemptDate { get; set; }

        public virtual Game Game { get; set; }

        public Attempt()
        {
            AttemptDate = DateTime.Now;
        }
    }
}