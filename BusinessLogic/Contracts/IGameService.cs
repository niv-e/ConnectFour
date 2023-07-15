using BusinessLogic.Responses;
using Model.Dtos;
using Model.Entities;

namespace BusinessLogic.Contracts
{
    public interface IGameService
    {
        Task<PlacePawnResponse> GetOpponentMove(Guid gameSessionId);
        Task<GameSessionDto?> GetSessionById(Guid sessionId);
        Task<IEnumerable<Tuple<int, int>>> TryToGetWinnerSequence(Guid gameSessionId);
        Task<PlacePawnResponse> PlacePawn(Guid gameSessionId, int colIndex);
        Task<GameSessionDto> StartGame(Player player);
    }
}