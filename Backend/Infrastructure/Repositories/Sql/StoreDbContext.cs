using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Sql
{
    /// <summary>
    /// Contexto de almacenamiento en base de datos. Aca se definen los nombres de 
    /// las tablas, y los mapeos entre los objetos
    /// </summary>
    internal class StoreDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; } 
        public DbSet<Game> Games { get; set; } 
        public DbSet<Attempt> Attempts { get; set; } 

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        protected StoreDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(p => p.PlayerId);
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.RegistrationDate).IsRequired();
            });
            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(g => g.GameId);
                entity.HasOne(g => g.Player)
                      .WithMany(p => p.Games)
                      .HasForeignKey(g => g.PlayerId);
                entity.Property(g => g.SecretNumber).IsRequired().HasMaxLength(4);
                entity.Property(g => g.CreatedAt).IsRequired();
            });
            modelBuilder.Entity<Attempt>(entity =>
            {
                entity.HasKey(a => a.AttemptId);
                entity.HasOne(a => a.Game)
                      .WithMany(g => g.Attempts)
                      .HasForeignKey(a => a.GameId);
                entity.Property(a => a.AttemptedNumber).IsRequired().HasMaxLength(4);
                entity.Property(a => a.AttemptDate).IsRequired();
            });
        }
    }
}
