using Model.Entities;

namespace BusinessLogic.Contracts
{
    public interface IGameSessionRepository
    {
        Task<GameSession?> GetSessionById(Guid guid);
        Task Insert(GameSession entity);
        Task SaveToDatabase();
        Task Update(GameSession entity);
    }
}