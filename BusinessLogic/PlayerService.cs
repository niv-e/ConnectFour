using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.Model.Boundaries;
using DAL.Contracts;
using DAL.Entities;
using Microsoft.Extensions.Logging;
using System.Numerics;

namespace BusinessLogic
{
    public class PlayerService : IPlayerService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PlayerService> _logger;
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(ILogger<PlayerService> logger, IPlayerRepository playerRepository, IMapper mapper)
        {
            _logger = logger;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }


        public async Task<PlayerBoundary> RegisterPlayer(PlayerBoundary playerBoundary)
        {
            try
            {
                _logger.LogDebug("Register player : {PlayerBoundary}", playerBoundary);

                Player player = _mapper.Map<PlayerBoundary, Player>(playerBoundary);

                _logger.LogDebug("Player Boundary maped to player : {Player}", player);

                await _playerRepository.InsertPlayer(player);

                _logger.LogDebug("Player : {Player} was inserted to the database", player);

                return _mapper.Map<PlayerBoundary>(player);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occuerred while try to insert a player to the database");
                throw;
            }
        }

        public async Task<PlayerBoundary?> GetPlayerById(int id)
        {
            Player? player = await _playerRepository.GetPlayerById(id);

            return _mapper.Map<PlayerBoundary>(player);

        }

        public async Task<bool> DeletePlayerById(int id)
        {
            await _playerRepository
                .DeletePlayer(id);

            return true;
        }

        public Task<bool> UpdatePlayer(PlayerBoundary playerBoundary)
        {
            Player player = _mapper.Map<PlayerBoundary, Player>(playerBoundary);

            return _playerRepository.UpdatePlayer(player);

        }
    }
}
