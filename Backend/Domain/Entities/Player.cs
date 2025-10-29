using Core.Domain.Entities;
using Domain.Validators;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad de dominio que representa un jugador en el sistema
    /// </summary>
    public class Player : DomainEntity<string, PlayerValidator>
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<Game> Games { get; set; }

        public Player()
        {
            Games = new List<Game>();
            RegistrationDate = DateTime.Now;
        }
    }
}