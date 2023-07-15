using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Contracts
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

