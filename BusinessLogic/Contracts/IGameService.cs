using Model.Dtos;
using Model.Entities;

namespace BusinessLogic.Contracts
{
    public interface IGameService
    {
        Task<GameSessionDto?> GetSessionById(Guid sessionId);
        Task<bool> PlacePawn(Guid gameSessionId, int colIndex);
        Task<GameSessionDto> StartGame(Player player);
    }
}