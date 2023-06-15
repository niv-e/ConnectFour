
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

        public GameService(IRepository<GameSession> gameSessionRepository)
        {
            GameSessionRepository = gameSessionRepository;
        }
        public bool PlacePawn(GameSession gameSession,Point point)
        {
            if (gameSession?.GameState?.IsPlayersTurn is false)
            {
                return false;
            }

            var valInPoint = gameSession.GameState?.GameBoard[point.X,point.Y];

            if (valInPoint.HasValue)
            {
                return false;
            }

            valInPoint = _playerNumber;

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
                    IsPlayersTurn = bool.Parse(Random.Shared.Next(1).ToString())
                },
                StaringTime = DateTime.Now
            };
        }
    }
}