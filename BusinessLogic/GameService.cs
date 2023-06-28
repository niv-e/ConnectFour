
using AutoMapper;
using BusinessLogic.Contracts;
using Model.Dtos;
using Model.Entities;
using System.Drawing;
using System.Text.Json;

namespace BusinessLogic
{
    public class GameService : IGameService
    {
        private readonly IRepository<GameSession> GameSessionRepository;
        private readonly IMapper _mapper;
        private const int _playerNumber = 1;
        private const int _serverNumber = 2;
        private int[] nextXPosition = { 0 };

        public GameService(IRepository<GameSession> gameSessionRepository, IMapper mapper)
        {
            GameSessionRepository = gameSessionRepository;
            _mapper = mapper;
        }
        public async Task<bool> PlacePawn(Guid gameSessionId, int colIndex)
        {
            var gameSessionEntity = await GameSessionRepository.GetById(gameSessionId);

            GameSessionDto gameSession = _mapper.Map<GameSessionDto>(gameSessionEntity);


            //if (gameSession?.GameState?.IsPlayersTurn is false)
            //{
            //    return false;
            //}

            var valInPoint = gameSession.GameState?.GameBoard[nextXPosition[colIndex - 1], colIndex];

            if (valInPoint.Value != 0)
            {
                return false;
            }

            valInPoint = _playerNumber;
            nextXPosition[colIndex - 1]++;
            return true;
        }

        public async Task<GameSessionDto?> GetSessionById(Guid sessionId)
        {
            GameSession? gameSession = await GameSessionRepository.GetById(sessionId);

            return _mapper.Map<GameSessionDto>(gameSession);

        }

        public async Task<GameSessionDto> StartGame(Player player)
        {
            GameSession session = CreateGameSession(player);

            await GameSessionRepository.Insert(session);

            return _mapper.Map<GameSessionDto>(session);
        }

        private GameSession CreateGameSession(Player player)
        {
            return new GameSession
            {
                Player = player,
                GameState = new()
                {
                    IsPlayersTurn = Random.Shared.Next(1) == 1 ? true : false,
                },
                StaringTime = DateTime.Now
            };
        }
    }
}