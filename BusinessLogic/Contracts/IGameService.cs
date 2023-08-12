using BusinessLogic.Model.Boundaries;
using BusinessLogic.Model.Dtos;
using BusinessLogic.Responses;

namespace BusinessLogic.Contracts
{
    public interface IGameService
    {
        Task<PlacePawnResponse> GetOpponentMove(Guid gameSessionId);
        Task<GameSessionDto?> GetSessionById(Guid sessionId);
        Task<IEnumerable<Tuple<int, int>>> TryToGetWinnerSequence(Guid gameSessionId);
        Task<PlacePawnResponse> PlacePawn(Guid gameSessionId, int colIndex);
        Task<GameSessionDto> StartGame(PlayerBoundary player);
        Task<bool> DeleteGameByGuid(Guid guid);
    }
}