using BusinessLogic.Contracts;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
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
        async Task<Player?> IPlayerRepository.GetPlayerById(int id)
        {
            var player = await _context.Players.FindAsync(id);
            await _context.SaveChangesAsync();
            return player;
        }

    }
}
