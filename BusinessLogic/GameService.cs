
using Model.Entities;

namespace BusinessLogic
{
    public class GameService
    {
        private readonly IRepository<GameSession> GameSessionRepository;
        public GameService(IRepository<GameSession> gameSessionRepository)
        {
            GameSessionRepository = gameSessionRepository;
        }

        public async Task<Guid> StartGame(Player player)
        {
            GameSession session = CreateGameSession(player);

            await GameSessionRepository.Insert(session);

            return session.SessionId;
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