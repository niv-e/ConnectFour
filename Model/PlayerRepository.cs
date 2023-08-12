using DAL.Contracts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DAL
{
    public class PlayerRepository : IPlayerRepository
    {
        private ConnectFourDbContext _context;

        public PlayerRepository(ConnectFourDbContext context) 
        {
            _context = context;
        }

        public async Task InsertPlayer(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }
        public async Task<Player?> GetPlayerById(int id)
        {
            var player = await _context.Players.FindAsync(id);
            return player;
        }

        public async Task DeletePlayer(int id)
        {
            await _context.Players
                .Where(p => p.PlayerId == id)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> UpdatePlayer(Player player)
        {
            var currentPlayer = _context.Players.Single(p => p.PlayerId == player.PlayerId);
           
            if (currentPlayer == null)
            {
                return false;
            }

            _context.Entry(currentPlayer)
                .CurrentValues
                .SetValues(player);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
