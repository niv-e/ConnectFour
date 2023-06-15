using AutoMapper;
using BusinessLogic.Contracts;
using Microsoft.Extensions.Logging;
using Model.bounderies;
using Model.Dtos;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PlayerService : IPlayerService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PlayerService> _logger;
        private readonly IRepository<Player> _playerRepository;

        public PlayerService(ILogger<PlayerService> logger, IRepository<Player> playerRepository, IMapper mapper)
        {
            _logger = logger;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }


        public async Task<Player> RegisterPlayer(PlayerBoundary playerBoundary)
        {
            try
            {
                _logger.LogDebug("Register player : {PlayerBoundary}", playerBoundary);

                Player player = _mapper.Map<PlayerBoundary, Player>(playerBoundary);

                _logger.LogDebug("Player Boundary maped to player : {Player}", player);

                await _playerRepository.Insert(player);

                _logger.LogDebug("Player : {Player} was inserted to the database", player);

                return player;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occuerred while try to insert a player to the database");
                throw;
            }
        }

        public Task<Player?> GetPlayerById(int id)
        {
            return _playerRepository.GetById(id);
        }
    }
}
