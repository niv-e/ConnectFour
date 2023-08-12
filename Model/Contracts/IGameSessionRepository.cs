using DAL.Entities;

namespace DAL.Contracts
{
    public interface IGameSessionRepository
    {
        Task<GameSession?> GetSessionById(Guid guid);
        Task<bool> DeleteGameById(Guid guid);
        Task Insert(GameSession entity);
        Task SaveToDatabase();
        Task Update(GameSession entity);
    }
}