using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ConnectFourDbContext : DbContext
    {
        public ConnectFourDbContext(DbContextOptions<ConnectFourDbContext> options) : base(options)
        {

        }
        public DbSet<GameSession> GameSessions { get; set; } = null!;
        public DbSet<GameState> GameStates { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                .HasData
                (
                    new Player
                    {
                        Country = "Israel",
                        FirstName = "Niv",
                        PhoneNumber = "1234567890",
                        PlayerId = 0
                    }
                );
            base.OnModelCreating(modelBuilder);
        }

    }
}

