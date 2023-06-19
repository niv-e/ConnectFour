
using AutoMapper;
using BusinessLogic.Contracts;
using Model.Dtos;
using Model.Entities;
using System.Drawing;

namespace BusinessLogic
{
    public class GameService
    {
        private readonly IRepository<GameSession> GameSessionRepository;
        private const int _playerNumber = 1;
        private const int _serverNumber = 2;
        private int[] nextXPosition = { 0 };

        public GameService(IRepository<GameSession> gameSessionRepository)
        {
            GameSessionRepository = gameSessionRepository;
        }
        public async Task<bool> PlacePawn(Guid gameSessionId,int colIndex)
        {
            var gameSession = await GameSessionRepository.GetById(gameSessionId);

            if (gameSession?.GameState?.IsPlayersTurn is false)
            {
                return false;
            }

            var valInPoint = gameSession.GameState?.GameBoard[nextXPosition[colIndex-1], colIndex];

            if (valInPoint.HasValue)
            {
                return false;
            }

            valInPoint = _playerNumber;
            nextXPosition[colIndex - 1]++;
            return true;
        }

        public Task<GameSession?> GetSessionById(Guid sessionId)
        {
            return GameSessionRepository.GetById(sessionId);

        }

        public async Task<GameSession> StartGame(Player player)
        {
            GameSession session = CreateGameSession(player);

            await GameSessionRepository.Insert(session);

            return session;
        }

        private GameSession CreateGameSession(Player player)
        {
            return new GameSession
            {
                Player = player,
                GameState = new GameState()
                {
                    IsPlayersTurn = Random.Shared.Next(1) == 1 ? true : false,
                    GameBoard = new int[6,7]
                },
                StaringTime = DateTime.Now
            };
        }
    }
}